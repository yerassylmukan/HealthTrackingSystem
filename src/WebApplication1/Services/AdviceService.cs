using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Common.Contracts;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class AdviceService : IAdviceService
{
    private readonly ApplicationDbContext _context;

    public AdviceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<DailyAdviceModel>> GetAdviceForTodayAsync(string userId)
    {
        var date = DateTime.UtcNow;

        var existing = await _context.UserAdviceLogs
            .Include(x => x.Advice)
            .WhereDateIs(date, x => x.DateShown)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (existing != null)
            return Result<DailyAdviceModel>.Success(new DailyAdviceModel { Text = existing.Advice.Text });

        var randomAdvice = await _context.DailyAdvices
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefaultAsync();

        if (randomAdvice == null)
            return Result<DailyAdviceModel>.Failure("No advice available.");

        Console.WriteLine(randomAdvice!.Text);

        _context.UserAdviceLogs.Add(new UserAdviceLog
        {
            UserId = userId,
            AdviceId = randomAdvice.Id,
            DateShown = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();

        return Result<DailyAdviceModel>.Success(new DailyAdviceModel { Text = randomAdvice.Text });
    }
}