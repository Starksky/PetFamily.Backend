namespace PetFamily.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error NotFound(Guid? guid = null) 
            => Error.NotFound("record.not.found", $"record{(guid != null ? $" with ID {guid}" : "")} not found.");
        public static Error IsAlreadyExists(string nameRecord = "record") 
            => Error.Conflict("record.is.already.exists", $"{nameRecord} is already exists.");
        
        public static Error Validation(string nameField = "field", string? message = null) 
            => Error.Validation("value.validation", $"{message ?? $"{nameField} is invalid."}", nameField);
        public static Error IsRequired(string nameField = "field") 
            => Error.Validation("value.is.required", $"{nameField} is required.", nameField);
    }
}