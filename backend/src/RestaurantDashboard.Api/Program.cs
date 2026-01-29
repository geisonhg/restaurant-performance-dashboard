using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Api.DTOs.Services;
using RestaurantDashboard.Api.DTOs.Services.Interfaces;
using RestaurantDashboard.Api.Services;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Infrastructure.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IExpensesService, ExpensesService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<ITipsService, TipsService>();
builder.Services.AddScoped<ITipRulesService, TipRulesService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ISummaryService, SummaryService>();





// Database connection (SQL Server example)
builder.Services.AddDbContext<DashboardDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Health check
builder.Services.AddHealthChecks();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DashboardDbContext>();
    // opcional: migrar automático
    await db.Database.MigrateAsync();
    await DbSeeder.SeedAsync(db);
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

