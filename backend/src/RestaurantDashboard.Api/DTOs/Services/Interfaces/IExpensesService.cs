using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Services.Interfaces
{
    public interface IExpensesService
    {
        Task<List<Expense>> GetAsync(DateTime? from, DateTime? to, string? category);
        Task<Expense?> GetByIdAsync(Guid id);
        Task<Expense> CreateAsync(Expense expense);
        Task<bool> UpdateAsync(Guid id, Expense updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
