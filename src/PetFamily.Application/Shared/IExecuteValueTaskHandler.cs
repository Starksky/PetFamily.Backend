namespace PetFamily.Application.Shared;

public interface IExecuteValueTaskHandler<in TData, TResult>
{
    public ValueTask<TResult> ExecuteAsync(TData createVolunteerRequest, CancellationToken cancellationToken = default);
}