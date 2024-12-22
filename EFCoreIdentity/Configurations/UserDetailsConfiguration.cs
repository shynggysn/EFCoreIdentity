using EFCoreIdentityDemo.Models;

namespace EFCoreIdentityDemo.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder.Property(ud => ud.FullName)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(ud => ud.Address)
            .HasMaxLength(500);
    }
}
