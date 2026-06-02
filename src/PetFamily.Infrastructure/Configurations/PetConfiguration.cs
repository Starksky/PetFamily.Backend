using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : AuditEntityConfiguration<Pet>
{
    public override void Configure(EntityTypeBuilder<Pet> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Pets");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .IsRequired()
            .HasConversion(id => id.Value,
                value => PetId.Create(value));
        
        builder.Property(p => p.SpeciesId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));
        
        builder.Property(p => p.BreedId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));


        builder.Property(p => p.Photos)
            .IsRequired(false)
            .HasConversion(
                p => p == null ? string.Empty : p.Serialize(),
                value => ValueObjectList<Photo>.Deserialize(value));
        
        builder.Property(p => p.Requisites)
            .IsRequired(false)
            .HasConversion(
                p => p == null ? string.Empty : p.Serialize(),
                value => ValueObjectList<Requisite>.Deserialize(value));

        /*builder.OwnsOne(x => x.Photos, ownsBuilder =>
        {
            ownsBuilder.ToJson();

            ownsBuilder.OwnsMany(x => x.Values, photoBuilder =>
            {
                photoBuilder.Property(p => p.PathToStorage)
                    .IsRequired()
                    .HasMaxLength(Constants.MaxLengthLowText);
            });
        });
        
        builder.OwnsOne(x => x.Requisites, ownsBuilder =>
        {
            ownsBuilder.ToJson();

            ownsBuilder.OwnsMany(p => p.Values, reqBuilder =>
            {
                reqBuilder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MaxLengthLowText);

                reqBuilder.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MaxLengthMediumText);
            });
        });*/
        
        builder.ComplexProperty(x => x.Description, propertyBuilder =>
        {
            propertyBuilder.IsRequired(false);
            propertyBuilder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxLengthHighText);
        });
        
        builder.Property(p => p.Color)
            .IsRequired(false)
            .HasMaxLength(Constants.MaxLengthLowValueText);
        
        builder.Property(p => p.HealthStatus)
            .IsRequired(false)
            .HasMaxLength(Constants.MaxLengthLowValueText);

        builder.ComplexProperty(p => p.Address, ownsBuilder =>
        {
            ownsBuilder.IsRequired(false);
            
            ownsBuilder.Property(a => a.PostalCode)
                .HasMaxLength(Constants.MaxLengthLowValueText);
            ownsBuilder.Property(a => a.City)
                .HasMaxLength(Constants.MaxLengthLowText);
            ownsBuilder.Property(a => a.Street)
                .HasMaxLength(Constants.MaxLengthLowText);
            ownsBuilder.Property(a => a.BuildingNumber);
            ownsBuilder.Property(a => a.BuildingNumberTwo);
            ownsBuilder.Property(a => a.ApartmentNumber);
        });

        builder.Property(p => p.Height)
            .IsRequired(false);
        
        builder.Property(p => p.Weight)
            .IsRequired(false);
        
        builder.Property(p => p.IsVaccinated)
            .IsRequired(false);
        
        builder.Property(p => p.IsNeutered)
            .IsRequired(false);

        builder.Property(p => p.HealthStatus)
            .IsRequired(false)
            .HasConversion<string>()
            .HasMaxLength(Constants.MaxLengthLowValueText);
        
        builder.Property(p => p.HelpStatus)
            .IsRequired(false)
            .HasConversion<string>()
            .HasMaxLength(Constants.MaxLengthLowValueText);
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
        
        builder.Property<bool>("_isPublished")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_published");
    }
}