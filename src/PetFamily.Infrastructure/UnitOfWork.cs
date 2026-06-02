using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Application.Shared;

namespace PetFamily.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}