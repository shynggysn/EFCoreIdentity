namespace EFCoreIdentityDemo.Models;

using Microsoft.AspNetCore.Identity;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public ApplicationUser User { get; set; } = default!;

    public IdentityRole Role { get; set; } = default!;
}
