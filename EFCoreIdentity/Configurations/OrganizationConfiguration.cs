using EFCoreIdentityDemo.Models;

namespace EFCoreIdentityDemo.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
