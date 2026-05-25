namespace PetFamily.Application.Shared;

public interface IExecuteTaskHandler<in TData, TResult>
{
    public Task<TResult> ExecuteAsync(TData createVolunteerRequest, CancellationToken cancellationToken = default);
}