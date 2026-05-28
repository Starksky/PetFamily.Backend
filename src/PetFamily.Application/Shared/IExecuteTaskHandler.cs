namespace PetFamily.Application.Shared;

public interface IExecuteTaskHandler<in TData, TResult>
{
    public Task<TResult> HandleAsync(TData createVolunteerRequest, CancellationToken cancellationToken = default);
}