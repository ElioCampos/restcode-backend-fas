

using Microsoft.EntityFrameworkCore;
using RestCode_WebApplication.Domain.Models;
using RestCode_WebApplication.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCode_WebApplication.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Category Entity
            builder.Entity<Category>().ToTable("Categories");

            // Constraints
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);

            builder.Entity<Category>()
                .HasMany(p => p.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);


            builder.Entity<Category>().HasData
                (
                    new Category { Id = 100, Name = "Comida Criolla", RestaurantId = 300 },
                    new Category { Id = 101, Name = "Comida Marina", RestaurantId = 301 }
                );

            // Product Entity
            builder.Entity<Product>().ToTable("Products");

            // Constraints
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(p => p.Price).IsRequired().HasMaxLength(3);

            //builder.Entity<Product>()
            //    .HasMany(p => p.SaleDetails)
            //    .WithOne(p => p.Product)
            //    .HasForeignKey(p => p.ProductId);

            builder.Entity<Product>().HasData
                (
                    new Product
                    { Id = 200, Name = "Arroz con Pollo", Price = 12.5, CategoryId = 100 },
                    new Product
                    { Id = 201, Name = "Ceviche", Price = 13.0, CategoryId = 101 }
                );

          
            //Apply Naming Conventions Policy
            builder.ApplySnakeCaseNamingConvention();
            
        }

    }
}
