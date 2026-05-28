using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        
        var entries = eventData.Context.ChangeTracker
            .Entries<ISoftDelete>()
            .Where(e => e is { State: EntityState.Deleted });

        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            entry.Entity.Delete();
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}