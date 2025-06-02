namespace WebApplication1.Models;

public class HealthEntryRequestModel
{
    public int Calories { get; set; }
    public int Steps { get; set; }
    public float SleepHours { get; set; }
    public string Mood { get; set; }
}