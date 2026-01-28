using System;


namespace RestaurantDashboard.Domain.Entities
{
    public class Import
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty; // Sales, Tips, Expenses
        public DateTime When { get; set; }
        public string FileName { get; set; } = string.Empty;
        public int TotalRows { get; set; }
        public int ValidRows { get; set; }
        public int InvalidRows { get; set; }
        public string? ErrorFilePath { get; set; }
    }
}


