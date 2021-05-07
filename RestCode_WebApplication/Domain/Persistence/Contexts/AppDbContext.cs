

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

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            //Restaurant Entity
            builder.Entity<Restaurant>().ToTable("Restaurants");

            // Constraints
            builder.Entity<Restaurant>().HasKey(p => p.Id);
            builder.Entity<Restaurant>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Restaurant>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Restaurant>().Property(p => p.Address).IsRequired().HasMaxLength(100);
            builder.Entity<Restaurant>().Property(p => p.CellPhoneNumber).IsRequired().HasMaxLength(9);


            builder.Entity<Restaurant>()
                .HasMany(p => p.Sales)
                .WithOne(p => p.Restaurant)
                .HasForeignKey(p => p.RestaurantId);

            builder.Entity<Restaurant>().HasData
                (
                    new Restaurant
                    { Id = 300, Name = "Pepito", Address = "Av. El Sol 345", CellPhoneNumber = 976823467, OwnerId=1},
                    new Restaurant
                    { Id = 301, Name = "McGrill", Address = "Av. Cutervo 231", CellPhoneNumber = 988746726, OwnerId = 2 }
                );


            // Owners Entity
            builder.Entity<Owner>().ToTable("Owners");

            // Constraints
            builder.Entity<Owner>().HasKey(p => p.Id);
            builder.Entity<Owner>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Owner>().Property(p => p.UserName).IsRequired();
            builder.Entity<Owner>().Property(p => p.FirstName).IsRequired();
            builder.Entity<Owner>().Property(p => p.LastName).IsRequired();
            builder.Entity<Owner>().Property(p => p.Cellphone).IsRequired();
            builder.Entity<Owner>().Property(p => p.Email).IsRequired();
            builder.Entity<Owner>().Property(p => p.Password).IsRequired();
            builder.Entity<Owner>().Property(p => p.Ruc).IsRequired().HasMaxLength(11);

            builder.Entity<Owner>()
                .HasOne(p => p.Restaurant)
                .WithOne(p => p.Owner)
                .HasForeignKey<Restaurant>(p => p.OwnerId);

            builder.Entity<Owner>().HasData
            (
                new Owner
                { Id = 1, UserName = "Luce00", FirstName = "Lucero", LastName = "Jara", Cellphone = "956819142", Email = "a@com", Password = "password", Ruc = 12345678901 },
                new Owner
                { Id = 2, UserName = "Iren23", FirstName = "Irene", LastName = "Soto", Cellphone = "945698822", Email = "b@com", Password = "P4sw0rd", Ruc = 12345678902 }
            );

            // Sales entity
            builder.Entity<Sale>().ToTable("Sales");

            // Constraints
            builder.Entity<Sale>().HasKey(p => p.Id);
            builder.Entity<Sale>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Sale>().Property(p => p.DateAndTime).IsRequired();
            builder.Entity<Sale>().Property(p => p.ClientFullName).IsRequired().HasMaxLength(100);

            builder.Entity<Sale>()
                .HasMany(p => p.SaleDetails)
                .WithOne(p => p.Sale)
                .HasForeignKey(p => p.SaleId);

            builder.Entity<Sale>().HasData
              (
                  new Sale
                  { Id = 150, DateAndTime = DateTime.Now.AddDays(4), ClientFullName = "Juan Perez", RestaurantId = 300 },
                  new Sale
                  { Id = 151, DateAndTime = DateTime.Now.AddDays(7), ClientFullName = "Jose Fulano", RestaurantId = 301 }

              );

            // Sale Details entity
            builder.Entity<SaleDetail>().ToTable("SalesDetails");

            // Constraints
            builder.Entity<SaleDetail>().HasKey(p => p.Id);
            builder.Entity<SaleDetail>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SaleDetail>().Property(p => p.Description);
            builder.Entity<SaleDetail>().Property(p => p.Quantity).IsRequired();

            builder.Entity<SaleDetail>().HasData
              (
                  new SaleDetail
                  { Id = 1, Description = "Salad", Quantity = 1, ProductId = 200, SaleId = 150 },
                  new SaleDetail
                  { Id = 2, Description = "Salad", Quantity = 2, ProductId = 201, SaleId = 151 }

              );

            //Apply Naming Conventions Policy
            builder.ApplySnakeCaseNamingConvention();
            
        }

    }
}
