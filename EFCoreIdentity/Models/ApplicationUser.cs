namespace EFCoreIdentityDemo.Models;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public UserDetails UserDetails { get; set; }
    public int? OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public virtual ICollection<IdentityRole> Roles { get; set; } = new List<IdentityRole>();


}
