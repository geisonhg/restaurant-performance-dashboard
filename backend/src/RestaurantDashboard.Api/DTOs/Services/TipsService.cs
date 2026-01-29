using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Api.DTOs.Tips;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Domain.Entities;
using RestaurantDashboard.Infrastructure.Data;

namespace RestaurantDashboard.Api.Services
{
    public class TipsService : ITipsService
    {
        private readonly DashboardDbContext _db;
        public TipsService(DashboardDbContext db) => _db = db;

        public async Task<List<Tip>> GetAsync(DateTime? from, DateTime? to, Guid? staffId)
        {
            var q = _db.Tips.AsQueryable();

            if (from.HasValue) q = q.Where(t => t.Date >= from.Value);
            if (to.HasValue) q = q.Where(t => t.Date <= to.Value);
            if (staffId.HasValue) q = q.Where(t => t.StaffId == staffId.Value);

            return await q.OrderByDescending(t => t.Date).ToListAsync();
        }

        public Task<Tip?> GetByIdAsync(Guid id) =>
            _db.Tips.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Tip> CreateAsync(Tip tip)
        {
            tip.Id = Guid.NewGuid();
            _db.Tips.Add(tip);
            await _db.SaveChangesAsync();
            return tip;
        }

        public async Task<bool> UpdateAsync(Guid id, Tip updated)
        {
            var existing = await _db.Tips.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            existing.Date = updated.Date;
            existing.StaffId = updated.StaffId;
            existing.Amount = updated.Amount;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _db.Tips.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            _db.Tips.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TipsSummaryDto> GetSummaryAsync(DateTime? from, DateTime? to, Guid? staffId)
        {
            var query = _db.Tips.AsQueryable();

            if (from.HasValue) query = query.Where(t => t.Date >= from.Value);
            if (to.HasValue) query = query.Where(t => t.Date <= to.Value);
            if (staffId.HasValue) query = query.Where(t => t.StaffId == staffId.Value);

            var count = await query.CountAsync();
            var total = await query.SumAsync(t => (decimal?)t.Amount) ?? 0m;
            var avg = count == 0 ? 0m : total / count;

            return new TipsSummaryDto
            {
                Count = count,
                TotalTips = total,
                AvgTip = avg
            };
        }

        public async Task<TipsDistributionDto> GetDistributionAsync(DateTime? from, DateTime? to)
        {
            // Tip total for period
            var tipsQuery = _db.Tips.AsQueryable();

            if (from.HasValue) tipsQuery = tipsQuery.Where(t => t.Date >= from.Value);
            if (to.HasValue) tipsQuery = tipsQuery.Where(t => t.Date <= to.Value);

            var totalTips = await tipsQuery.SumAsync(t => (decimal?)t.Amount) ?? 0m;

            // Active rule (if none, default 100/0 to avoid breaking)
            var rule = await _db.TipRules
                .OrderByDescending(r => r.ValidFrom)
                .FirstOrDefaultAsync(r => r.ValidTo == null);

            var fohPercent = rule?.FOHPercent ?? 100m;
            var bohPercent = rule?.BOHPercent ?? 0m;

            var fohAmount = totalTips * (fohPercent / 100m);
            var bohAmount = totalTips * (bohPercent / 100m);

            return new TipsDistributionDto
            {
                TotalTips = totalTips,
                FOHPercent = fohPercent,
                BOHPercent = bohPercent,
                FOHAmount = decimal.Round(fohAmount, 2),
                BOHAmount = decimal.Round(bohAmount, 2)
            };
        }
    }
}
