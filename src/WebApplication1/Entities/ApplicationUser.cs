using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public float Weight { get; set; }

    public float Height { get; set; }

    public string Goal { get; set; } = string.Empty;

    public ICollection<HealthEntry> HealthEntries { get; set; } = new List<HealthEntry>();
}