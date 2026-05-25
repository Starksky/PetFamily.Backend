using Newtonsoft.Json;

namespace PetFamily.Domain.Shared;

public record Error
{
    public string Code { get; }
    public string Message { get; }
    public string? PropertyName { get; }
    public EErrorType ErrorType { get; }
    
    [JsonConstructor]
    private Error(string code, string message, EErrorType errorType, string? propertyName = null)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
        PropertyName = propertyName;
    }

    public string Serialize() => JsonConvert.SerializeObject(this);
    public static Error Deserialize(string json) 
    {
         Error? error = JsonConvert.DeserializeObject<Error>(json);
         if (error == null)
             throw new InvalidOperationException();
         return error;
    }
    
    public static Error Create(string code, string message)
        => new Error(code, message, EErrorType.Unknown);
    public static Error Validation(string code, string message, string? propertyName = null)
        => new Error(code, message, EErrorType.Validation, propertyName);
    public static Error NotFound(string code, string message)
        => new Error(code, message, EErrorType.NotFound);
    public static Error Conflict(string code, string message)
        => new Error(code, message, EErrorType.Conflict);
    public static Error Failure(string code, string message)
        => new Error(code, message, EErrorType.Failure);
}

public enum EErrorType
{
    Unknown,
    Validation,
    NotFound,
    Conflict,
    Failure
}