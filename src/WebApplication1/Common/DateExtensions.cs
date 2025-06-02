using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Common;

public static class DateExtensions
{
    public static IQueryable<T> WhereDateIs<T>(
        this IQueryable<T> query,
        DateTime date,
        Expression<Func<T, DateTime>> dateSelector)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        return query.Where(item =>
            EF.Property<DateTime>(item!, ((MemberExpression)dateSelector.Body).Member.Name) >= startDate &&
            EF.Property<DateTime>(item!, ((MemberExpression)dateSelector.Body).Member.Name) < endDate);
    }
}