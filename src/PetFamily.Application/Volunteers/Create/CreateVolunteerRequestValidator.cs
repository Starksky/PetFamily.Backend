using FluentValidation;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.Create;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(x => x.Email).MustBeValueObject(Email.Create);
        RuleFor(x => x.Phone).MustBeValueObject(Phone.Create);
        RuleFor(x => x.Fio)
            .MustBeValueObject(x => Fio.Create(x.FirstName, x.LastName, x.Patronymic));
    }
}