using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Configurations;

public class VolunteersConfiguration : AuditEntityConfiguration<Volunteer>
{
    public override void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        base.Configure(builder);
        
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

        builder.ComplexProperty(x => x.Phone, propertyBuilder =>
        {
            propertyBuilder.IsRequired();
            propertyBuilder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxLengthLowValueText);
        });
        
        builder.ComplexProperty(x => x.Email, propertyBuilder =>
        {
            propertyBuilder.IsRequired();
            propertyBuilder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxLengthLowValueText);
        });

        builder.ComplexProperty(x => x.Description, propertyBuilder =>
        {
            propertyBuilder.IsRequired(false);
            propertyBuilder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxLengthHighText);
        });
        
        builder.ComplexProperty(x => x.JobAge, propertyBuilder =>
        {
            propertyBuilder.IsRequired(false);
            propertyBuilder.Property(p => p.Value)
                .IsRequired();
        });
        
        builder.HasMany(x => x.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

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
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}