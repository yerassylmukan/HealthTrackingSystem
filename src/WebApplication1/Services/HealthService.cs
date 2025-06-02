using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Common.Contracts;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class HealthService : IHealthService
{
    private readonly ApplicationDbContext _context;

    public HealthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> SaveDailyDataAsync(string userId, HealthEntryRequestModel healthEntryRequest)
    {
        var existing = await _context.HealthEntries
            .Where(x => x.UserId == userId)
            .WhereDateIs(DateTime.UtcNow, x => x.Date)
            .FirstOrDefaultAsync();

        if (existing != null)
        {
            existing.Calories = healthEntryRequest.Calories;
            existing.Steps = healthEntryRequest.Steps;
            existing.SleepHours = healthEntryRequest.SleepHours;
            existing.Mood = healthEntryRequest.Mood;
        }
        else
        {
            var entry = new HealthEntry
            {
                UserId = userId,
                Date = DateTime.UtcNow,
                Calories = healthEntryRequest.Calories,
                Steps = healthEntryRequest.Steps,
                SleepHours = healthEntryRequest.SleepHours,
                Mood = healthEntryRequest.Mood
            };
            _context.HealthEntries.Add(entry);
        }

        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<HealthEntryResponseModel>> GetDailyDataAsync(string userId)
    {
        var entry = await _context.HealthEntries
            .Where(x => x.UserId == userId)
            .WhereDateIs(DateTime.UtcNow, x => x.Date)
            .FirstOrDefaultAsync();

        if (entry == null)
            return Result<HealthEntryResponseModel>.Failure("No data found.");

        var dto = new HealthEntryResponseModel
        {
            Date = entry.Date,
            Calories = entry.Calories,
            Steps = entry.Steps,
            SleepHours = entry.SleepHours,
            Mood = entry.Mood
        };

        return Result<HealthEntryResponseModel>.Success(dto);
    }

    public async Task<Result<IEnumerable<HealthEntryResponseModel>>> GetAllDailyDataAsync(string userId)
    {
        var entries = await _context.HealthEntries.ToListAsync();

        var result = entries.Select(entry => new HealthEntryResponseModel
        {
            Date = entry.Date,
            Calories = entry.Calories,
            Steps = entry.Steps,
            SleepHours = entry.SleepHours,
            Mood = entry.Mood
        });

        return Result<IEnumerable<HealthEntryResponseModel>>.Success(result);
    }
}