using System;

namespace RestaurantDashboard.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Section { get; set; }
        public int Covers { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public string? ServiceType { get; set; } // dine-in, delivery
    }

}

