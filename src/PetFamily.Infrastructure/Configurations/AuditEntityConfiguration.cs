using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations;

public abstract class AuditEntityConfiguration<TEntity> 
    : IEntityTypeConfiguration<TEntity> 
    where TEntity : class, IHasTimestamps
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now() at time zone 'utc'");
        
        builder.Property(e => e.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now() at time zone 'utc'");
    }
}