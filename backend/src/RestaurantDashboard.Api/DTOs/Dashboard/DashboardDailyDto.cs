namespace RestaurantDashboard.Api.DTOs.Dashboard
{
    public class DashboardDailyDto
    {
        public DateTime Date { get; set; }

        public decimal Sales { get; set; }
        public decimal Expenses { get; set; }
        public decimal Tips { get; set; }

        // Simple profit definition: Sales - Expenses
        public decimal NetProfit { get; set; }
    }
}
