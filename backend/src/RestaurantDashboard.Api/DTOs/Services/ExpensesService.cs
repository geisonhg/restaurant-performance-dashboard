using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Domain.Entities;
using RestaurantDashboard.Infrastructure.Data;
using RestaurantDashboard.Api.Services.Interfaces;

namespace RestaurantDashboard.Api.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly DashboardDbContext _db;

        public ExpensesService(DashboardDbContext db) => _db = db;

        public async Task<List<Expense>> GetAsync(DateTime? from, DateTime? to, string? category)
        {
            var q = _db.Expenses.AsQueryable();

            if (from.HasValue) q = q.Where(e => e.Date >= from.Value);
            if (to.HasValue) q = q.Where(e => e.Date <= to.Value);
            if (!string.IsNullOrWhiteSpace(category)) q = q.Where(e => e.Category == category);

            return await q.OrderByDescending(e => e.Date).ToListAsync();
        }

        public Task<Expense?> GetByIdAsync(Guid id) =>
            _db.Expenses.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Expense> CreateAsync(Expense expense)
        {
            expense.Id = Guid.NewGuid();
            _db.Expenses.Add(expense);
            await _db.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> UpdateAsync(Guid id, Expense updated)
        {
            var existing = await _db.Expenses.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            existing.Date = updated.Date;
            existing.Category = updated.Category;
            existing.Amount = updated.Amount;
            existing.Notes = updated.Notes;
            existing.Recurring = updated.Recurring;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _db.Expenses.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            _db.Expenses.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
