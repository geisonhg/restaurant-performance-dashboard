namespace RestaurantDashboard.Api.DTOs.Sales
{
    public class SalesSummaryDto
    {
        public int Count { get; set; }
        public int TotalCovers { get; set; }

        public decimal TotalNet { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalGross { get; set; }

        public decimal AvgNetPerCover { get; set; }
    }
}
