using MassTransitWithAzureServiceBus.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MassTransitWithAzureServiceBus.Api.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        private readonly bool _created;
        public DbSet<Order> Orders {  get; set; }
        public DbSet<Product> Products {  get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            if(!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasData(
                new Order
                {
                    OrderId = 1,
                    CustomerId = "12",
                    CustomerName = "Muhammet Andiç",
                    CustomerPhone = "766567",
                });
            builder.Entity<Product>()
                .HasData(new Product { ProductId = 1, ProductName = "Bisküvi", Quantity = 3, Price = 2.3M, OrderId = 1 });
            builder.Entity<Product>()
                .HasData(new Product { ProductId = 2, ProductName = "Çikolata", Quantity = 3, Price = 1.45M, OrderId = 1 });
            base.OnModelCreating(builder);
        }
    }
}
