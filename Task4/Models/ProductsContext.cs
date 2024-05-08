using Microsoft.EntityFrameworkCore;

namespace Task4.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product { Id = 1, Name = "Яблоко", Price = 49, Quantity = 50 },
                    new Product { Id = 2, Name = "Вода", Price = 69, Quantity = 50 },
                    new Product { Id = 3, Name = "Лопата", Price = 599, Quantity = 10 },
                    new Product { Id = 4, Name = "Кактус", Price = 259, Quantity = 10 }
            );
        }

        public DbSet<Product> Products { get; set; }
    }
}
