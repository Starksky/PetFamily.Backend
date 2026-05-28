using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        
        // fix don't tracked changes
        // force call DetectChanges 
        eventData.Context.ChangeTracker.DetectChanges();
        
        var entries = eventData.Context.ChangeTracker.Entries<IHasTimestamps>()
            .Where(e => e is { State: EntityState.Modified });
        
        foreach (var entry in entries)
            entry.Entity.Update();
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}