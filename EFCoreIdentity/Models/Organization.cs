﻿namespace EFCoreIdentityDemo.Models;

public class Organization
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}
