using WebApplication1.Models;

namespace WebApplication1.Common.Contracts;

public interface IAdviceService
{
    Task<Result<DailyAdviceModel>> GetAdviceForTodayAsync(string userId);
}