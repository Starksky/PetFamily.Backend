using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.UploadPetPhotos;

public class UploadPetPhotosHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<UploadPetPhotosHandler> _logger;
    
    public UploadPetPhotosHandler(
        IVolunteersRepository volunteersRepository, 
        IFileProvider fileProvider,
        ILogger<UploadPetPhotosHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _fileProvider = fileProvider;
    }

    public async Task<Result<UploadPetPhotosResponse, Error>> HandleAsync(UploadPetPhotosCommand command, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var volunteer = volunteerResult.Value;
        
        var petId = PetId.Create(command.PetId);
        var petResult = volunteer.GetPet(petId);
        if (petResult.IsFailure)
            return petResult.Error;
        
        var pet = petResult.Value;
        
        var uploadResult = await _fileProvider.UploadFilesAsync(command.PhotosUploadArgs, cancellationToken);
        var photosResults = uploadResult.Item1
            .Select(result => Photo.Create(result.InternalName))
            .ToArray();
        
        var photos = photosResults
            .Where(result => result.IsSuccess)
            .Select(result => result.Value).
            ToArray();
        
        pet.AddPhotos(photos);

        var addPhotos = photos.Select(p => p.PathToStorage);
        
        var photosResult = uploadResult.Item1
            .Where(result => addPhotos.Contains(result.InternalName));
        
        var errorsResult = uploadResult.Item2.ToList();
        errorsResult.AddRange(photosResults
            .Where(result => result.IsFailure)
            .Select(result => result.Error));
        
        await _volunteersRepository.SaveAsync(volunteer, cancellationToken);
        
        _logger.LogInformation("Volunteer with id {Id} uploaded photos to pet with id {PetId}",  volunteer.Id.Value, petId.Value);
        
        return new UploadPetPhotosResponse(photosResult, errorsResult);
    }
}

public record UploadPetPhotosResponse(IEnumerable<FileUploadResult> Photos, IEnumerable<Error> Errors);

public record UploadPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<FileUploadArgs> PhotosUploadArgs
);

public class UploadPetPhotosCommandValidator : AbstractValidator<UploadPetPhotosCommand>
{
    public UploadPetPhotosCommandValidator()
    {
        RuleFor(v => v.VolunteerId).NotEmpty();
        RuleFor(v => v.PetId).NotEmpty();
        RuleFor(v => v.PhotosUploadArgs).NotEmpty();
    }
}