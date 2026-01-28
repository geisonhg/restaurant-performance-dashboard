using System;

namespace RestaurantDashboard.Api.DTOs.Expenses
{
    public class CreateExpenseDto
    {
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public bool Recurring { get; set; }
    }
}
