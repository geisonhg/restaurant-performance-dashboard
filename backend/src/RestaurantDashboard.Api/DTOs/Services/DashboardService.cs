using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Api.DTOs.Dashboard;
using RestaurantDashboard.Api.DTOs.Services.Interfaces;
using RestaurantDashboard.Infrastructure.Data;

namespace RestaurantDashboard.Api.DTOs.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly DashboardDbContext _db;

        public DashboardService(DashboardDbContext db)
        {
            _db = db;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync(DateTime? from, DateTime? to)
        {
            // If user does not provide dates, use last 30 days by default
            var end = (to ?? DateTime.UtcNow).Date;
            var start = (from ?? end.AddDays(-30)).Date;

            // Include full "to" day (end-of-day)
            var toExclusive = end.AddDays(1);

            var totalSales = await _db.Sales
                .Where(s => s.Date >= start && s.Date < toExclusive)
                .SumAsync(s => s.Amount);

            var totalExpenses = await _db.Expenses
                .Where(e => e.Date >= start && e.Date < toExclusive)
                .SumAsync(e => e.Amount);

            var totalTips = await _db.Tips
                .Where(t => t.Date >= start && t.Date < toExclusive)
                .SumAsync(t => t.Amount);

            var netProfit = totalSales - totalExpenses;

            var tipsPercent = totalSales == 0
                ? 0
                : Math.Round((totalTips / totalSales) * 100m, 2);

            return new DashboardSummaryDto
            {
                From = start,
                To = end,
                TotalSales = totalSales,
                TotalExpenses = totalExpenses,
                TotalTips = totalTips,
                NetProfit = netProfit,
                TipsPercentOfSales = tipsPercent
            };
        }

        public async Task<List<DashboardDailyDto>> GetDailyAsync(DateTime? from, DateTime? to)
        {
            // If user does not provide dates, use last 14 days by default
            var end = (to ?? DateTime.UtcNow).Date;
            var start = (from ?? end.AddDays(-14)).Date;

            var toExclusive = end.AddDays(1);

            // Group sales by day
            var salesByDay = await _db.Sales
                .Where(s => s.Date >= start && s.Date < toExclusive)
                .GroupBy(s => s.Date.Date)
                .Select(g => new { Date = g.Key, Total = g.Sum(x => x.Amount) })
                .ToListAsync();

            // Group expenses by day
            var expensesByDay = await _db.Expenses
                .Where(e => e.Date >= start && e.Date < toExclusive)
                .GroupBy(e => e.Date.Date)
                .Select(g => new { Date = g.Key, Total = g.Sum(x => x.Amount) })
                .ToListAsync();

            // Group tips by day
            var tipsByDay = await _db.Tips
                .Where(t => t.Date >= start && t.Date < toExclusive)
                .GroupBy(t => t.Date.Date)
                .Select(g => new { Date = g.Key, Total = g.Sum(x => x.Amount) })
                .ToListAsync();

            // Build a complete day list (even if some days have 0 values)
            var result = new List<DashboardDailyDto>();
            for (var day = start; day <= end; day = day.AddDays(1))
            {
                var sales = salesByDay.FirstOrDefault(x => x.Date == day)?.Total ?? 0m;
                var expenses = expensesByDay.FirstOrDefault(x => x.Date == day)?.Total ?? 0m;
                var tips = tipsByDay.FirstOrDefault(x => x.Date == day)?.Total ?? 0m;

                result.Add(new DashboardDailyDto
                {
                    Date = day,
                    Sales = sales,
                    Expenses = expenses,
                    Tips = tips,
                    NetProfit = sales - expenses
                });
            }

            return result;
        }
    }
}
