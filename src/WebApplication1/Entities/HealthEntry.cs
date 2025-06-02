namespace WebApplication1.Entities;

public class HealthEntry
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int Calories { get; set; }

    public int Steps { get; set; }

    public float SleepHours { get; set; }

    public string Mood { get; set; } = string.Empty;

    public string UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;
}