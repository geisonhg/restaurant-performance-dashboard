using System;


namespace RestaurantDashboard.Domain.Entities
{
    public class Staff
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool Active { get; set; }
    }


}

