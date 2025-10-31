using BugStore.Domain.Entities;
using BugStore.Domain.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;

namespace BugStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderLine> OrderLines { get; set; } = null!;
        public DbSet<BestCustomers> BestCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<BestCustomers>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("vw_report_best_customers");
                });

            modelBuilder
               .Entity<RevenueByPeriodResult>(eb =>
               {
                   eb.HasNoKey();
                   eb.ToView("vw_report_revenue_by_period");
               });
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var databaseUrl = Environment.GetEnvironmentVariable("ConnectionStrings__Default");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            if (!string.IsNullOrEmpty(databaseUrl))
            {
                if (databaseUrl.StartsWith("postgresql://"))
                    databaseUrl = databaseUrl.Replace("postgresql://", "postgres://");

                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');

                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = databaseUri.Host,
                    Port = databaseUri.Port > 0 ? databaseUri.Port : 5432,
                    Username = userInfo[0],
                    Password = userInfo.Length > 1 ? userInfo[1] : "",
                    Database = databaseUri.AbsolutePath.TrimStart('/'),
                    SslMode = SslMode.Require,
                    TrustServerCertificate = true
                };

                optionsBuilder.UseNpgsql(builder.ConnectionString);
            }
            else
            {
                optionsBuilder.UseSqlite("Data Source=bugstore-app.db");
            }

            return new AppDbContext(optionsBuilder.Options);
        }

    }
}
