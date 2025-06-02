using WebApplication1.Models;

namespace WebApplication1.Common.Contracts;

public interface IHealthService
{
    Task<Result> SaveDailyDataAsync(string userId, HealthEntryRequestModel healthEntryResponseModel);
    Task<Result<HealthEntryResponseModel>> GetDailyDataAsync(string userId);
    Task<Result<IEnumerable<HealthEntryResponseModel>>> GetAllDailyDataAsync(string userId);
}