namespace PetFamily.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error NotFound(Guid? guid = null) => Error.NotFound("record.not.found", $"record{(guid != null ? $" with ID {guid}" : "")} not found");
        public static Error Validation(string nameField = "name") => Error.Validation("value.validation", $"{nameField} is invalid", nameField);
        public static Error IsRequired(string nameField = "name") => Error.Validation("value.is.required", $"{nameField} is required", nameField);
        public static Error IsAlreadyExists(string nameField = "name") => Error.Conflict("value.is.already.exists", $"{nameField} is already exists");
    }
}