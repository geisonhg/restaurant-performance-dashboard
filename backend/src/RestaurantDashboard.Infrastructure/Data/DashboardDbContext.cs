using System;
using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Domain.Entities;


namespace RestaurantDashboard.Infrastructure.Data
{
    public class DashboardDbContext : DbContext
    {
        public DashboardDbContext(DbContextOptions<DashboardDbContext> options) : base(options) { }

        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<Tip> Tips => Set<Tip>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Staff> Staff => Set<Staff>();
        public DbSet<TipRule> TipRules => Set<TipRule>();
        public DbSet<Import> Imports => Set<Import>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Montos de dinero
            modelBuilder.Entity<Sale>(e =>
            {
                e.Property(p => p.NetAmount).HasColumnType("decimal(18,2)");
                e.Property(p => p.Tax).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Tip>(e =>
            {
                e.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Expense>(e =>
            {
                e.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            });

            // Porcentajes de propinas
            modelBuilder.Entity<TipRule>(e =>
            {
                e.Property(p => p.FOHPercent).HasColumnType("decimal(5,2)");
                e.Property(p => p.BOHPercent).HasColumnType("decimal(5,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}