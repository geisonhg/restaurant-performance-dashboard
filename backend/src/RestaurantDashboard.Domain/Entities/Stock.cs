using System;


namespace RestaurantDashboard.Domain.Entities
{
    public class Stock
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal CurrentQty { get; set; }
        public decimal LowStockThreshold { get; set; }
    }

}


