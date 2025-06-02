namespace WebApplication1.Entities;

public class UserAdviceLog
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public int AdviceId { get; set; }

    public DailyAdvice Advice { get; set; } = null!;

    public DateTime DateShown { get; set; }
}