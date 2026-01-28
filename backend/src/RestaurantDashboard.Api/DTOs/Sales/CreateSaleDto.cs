using System;

namespace RestaurantDashboard.Api.DTOs.Sales
{
    public class CreateSaleDto
    {
        public DateTime Date { get; set; }
        public string? Section { get; set; }
        public int Covers { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Tax { get; set; }
        public string? ServiceType { get; set; } // dine-in/delivery
    }
}
