namespace EFCoreIdentity.Dto;

public class CreateUserRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? OrganizationName { get; set; }
    public List<string>? Roles { get; set; }
}
