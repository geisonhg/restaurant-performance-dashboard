using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Infrastructure.Data;
using RestaurantDashboard.Api.Services;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Api.Services;
using RestaurantDashboard.Api.Services.Interfaces;



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

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

