using PetFamily.Domain.Shared;

namespace PetFamily.API.Response;

public record EnvelopeError(string ErrorCode, string ErrorMessage, string? PropertyName = null);

public record Envelope
{
    public object? Result { get; }
    public List<EnvelopeError> Errors { get; }
    public DateTime Timestamp { get; }
    
    private Envelope(object? result, IEnumerable<EnvelopeError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        Timestamp = DateTime.UtcNow;
    }

    public static Envelope Ok(object result)
        => new(result, []);
    public static Envelope Error(Error error)
        => new(null, [new EnvelopeError(error.Code, error.Message, error.PropertyName)]);
    public static Envelope Error(IEnumerable<Error> errors)
        => new(null, errors.Select(e => new EnvelopeError(e.Code, e.Message, e.PropertyName)));
    public static Envelope Error(IEnumerable<EnvelopeError> errors)
        => new(null, errors);
}