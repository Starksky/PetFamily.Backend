using FluentValidation;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.UpdateInfo;


public class UpdateVolunteerInfoDtoValidator : AbstractValidator<UpdateVolunteerInfoDto>
{
    public UpdateVolunteerInfoDtoValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}

public class UpdateVolunteerInfoRequestValidator : AbstractValidator<UpdateVolunteerInfoRequest>
{
    public UpdateVolunteerInfoRequestValidator()
    {
        RuleFor(r => r.Description).MustBeValueObject(Description.Create); //.When(r => !string.IsNullOrWhiteSpace(r.Description)); for not require field
        RuleFor(r => r.JobAge).MustBeValueObject(JobAge.Create);
    }
}