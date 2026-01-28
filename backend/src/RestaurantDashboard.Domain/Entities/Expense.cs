using System;



namespace RestaurantDashboard.Domain.Entities
{
    public class Expense
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public bool Recurring { get; set; }
    }
}
