using EFCoreIdentityDemo.Models;
using Microsoft.AspNetCore.Identity;

namespace EFCoreIdentityDemo.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasOne(u => u.UserDetails)
            .WithOne(ud => ud.User)
            .HasForeignKey<UserDetails>(ud => ud.UserId);
        
        builder.HasOne(u => u.Organization)
            .WithMany(o => o.Users)
            .HasForeignKey(u => u.OrganizationId);
        
        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<string>>(
                userRole => userRole
                    .HasOne<IdentityRole>() // Use the default IdentityRole
                    .WithMany()
                    .HasForeignKey(ur => ur.RoleId),
                userRole => userRole
                    .HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(ur => ur.UserId),
                userRole =>
                {
                    userRole.ToTable("AspNetUserRoles"); // Map to the default join table
                }
            );
    }
}
