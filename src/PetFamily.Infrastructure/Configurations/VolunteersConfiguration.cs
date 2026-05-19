using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Configurations;

public class VolunteersConfiguration : IEntityTypeConfiguration<Volunteer>
{

    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("Volunteers");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.ComplexProperty(x => x.Fio, propertyBuilder =>
        {
            propertyBuilder.IsRequired();
            
            propertyBuilder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MaxLengthLowValueText);
            propertyBuilder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(Constants.MaxLengthLowValueText);
            propertyBuilder.Property(p => p.Patronymic)
                .IsRequired(false)
                .HasMaxLength(Constants.MaxLengthLowValueText);
        });
        
        builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(Constants.MaxLengthLowValueText);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(Constants.MaxLengthLowValueText);
        
        
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(Constants.MaxLengthHighText);
        
        builder.Property(x => x.JobAge)
            .IsRequired(false);
        
        builder.HasMany(x => x.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");

        builder.OwnsOne(x => x.VolunteerDetails, ownsBuilder =>
        {
            ownsBuilder.ToJson();

            ownsBuilder.OwnsMany(p => p.Requisites, reqBuilder =>
            {
                reqBuilder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MaxLengthLowText);

                reqBuilder.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MaxLengthMediumText);
            });

            ownsBuilder.OwnsMany(x => x.SocialLinks, socBuilder =>
            {
                socBuilder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MaxLengthLowText);
                socBuilder.Property(p => p.Url)
                    .IsRequired()
                    .HasMaxLength(Constants.MaxLengthMediumText);
            });
        });
    }
}