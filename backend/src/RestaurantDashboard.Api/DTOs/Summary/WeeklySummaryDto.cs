namespace RestaurantDashboard.Api.DTOs.Summary
{
    public class WeeklySummaryDto
    {
        public decimal TotalNetSales { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalTips { get; set; }
        public int TotalCovers { get; set; }

        public string? TopSection { get; set; }
        public decimal TopSectionNetSales { get; set; }

        public decimal FOHTipsShare { get; set; }
        public decimal BOHTipsShare { get; set; }
    }
}
