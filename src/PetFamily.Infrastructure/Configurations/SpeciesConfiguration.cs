using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{

    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("Species");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .IsRequired()
            .HasConversion(id => id.Value,
                value => SpeciesId.Create(value));

        builder.Property(s => s.Name)
            .IsRequired(true)
            .HasMaxLength(Constants.MaxLengthLowText);
        
        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}