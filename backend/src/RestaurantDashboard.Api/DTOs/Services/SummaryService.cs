using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Api.DTOs.Services.Interfaces;
using RestaurantDashboard.Api.DTOs.Summary;
using RestaurantDashboard.Infrastructure.Data;

namespace RestaurantDashboard.Api.DTOs.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly DashboardDbContext _db;

        public SummaryService(DashboardDbContext db) => _db = db;

        public async Task<WeeklySummaryDto> GetWeeklySummaryAsync(DateTime from, DateTime to)
        {
            // Load sales and tips in the date range
            var sales = await _db.Sales
                .Where(s => s.Date >= from && s.Date <= to)
                .ToListAsync();

            var tips = await _db.Tips
                .Where(t => t.Date >= from && t.Date <= to)
                .ToListAsync();

            // Get active tip rule (optional)
            var rule = await _db.TipRules
                .OrderByDescending(r => r.ValidFrom)
                .FirstOrDefaultAsync(r => r.ValidFrom <= to && (r.ValidTo == null || r.ValidTo >= from));

            var totalNetSales = sales.Sum(s => s.Amount);
            var totalTax = sales.Sum(s => s.Tax);
            var totalCovers = sales.Sum(s => s.Covers);
            var totalTips = tips.Sum(t => t.Amount);

            // Top section
            var topSectionGroup = sales
                .Where(s => !string.IsNullOrWhiteSpace(s.Section))
                .GroupBy(s => s.Section!)
                .Select(g => new { Section = g.Key, Net = g.Sum(x => x.Amount) })
                .OrderByDescending(x => x.Net)
                .FirstOrDefault();

            var fohPercent = rule?.FOHPercent ?? 0;
            var bohPercent = rule?.BOHPercent ?? 0;

            return new WeeklySummaryDto
            {
                TotalNetSales = totalNetSales,
                TotalTax = totalTax,
                TotalCovers = totalCovers,
                TotalTips = totalTips,

                TopSection = topSectionGroup?.Section,
                TopSectionNetSales = topSectionGroup?.Net ?? 0,

                FOHTipsShare = totalTips * (fohPercent / 100m),
                BOHTipsShare = totalTips * (bohPercent / 100m),
            };
        }
    }
}
