using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(DashboardDbContext db)
    {
        // Si ya hay staff, asumimos seed hecho
        if (db.Staff.Any()) return;

        var staff = new List<Staff>
        {
            new() { Id = Guid.NewGuid(), Name = "Ana" },
            new() { Id = Guid.NewGuid(), Name = "Luis" },
            new() { Id = Guid.NewGuid(), Name = "Sofia" },
            new() { Id = Guid.NewGuid(), Name = "Carlos" },
        };
        db.Staff.AddRange(staff);

        // Sales / Tips / Expenses básicos (ajusta a tus entities reales)
        // Aquí dejo la idea: crea una semana con patrón
        var start = DateTime.Today.AddDays(-14); // 2 semanas atrás
        var random = new Random();

        for (int d = 0; d < 14; d++)
        {
            var day = start.AddDays(d);
            var isWeekend = day.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday;

            // Ejemplo tips
            db.Tips.Add(new Tip
            {
                Id = Guid.NewGuid(),
                Date = day,
                StaffId = staff[random.Next(staff.Count)].Id,
                Amount = isWeekend ? random.Next(80, 200) : random.Next(10, 60)
            });

            db.Sales.Add(new Sale
            {
                Id = Guid.NewGuid(),
                Date = day,

                // Higher sales on weekends, lower on weekdays
                Amount = isWeekend
                ? random.Next(2500, 5500)
                : random.Next(800, 2200)
            });


            // Ejemplo expenses
            db.Expenses.Add(new Expense
            {
                Id = Guid.NewGuid(),
                Date = day,
                Amount = random.Next(50, 250),
                Category = "Supplies"
            });
        }

        await db.SaveChangesAsync();
    }
}
