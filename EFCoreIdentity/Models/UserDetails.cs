namespace EFCoreIdentityDemo.Models;

public class UserDetails
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
