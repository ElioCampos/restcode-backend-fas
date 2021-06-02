

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
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Consultancy> Consultancies { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Consultant Entity
            builder.Entity<Consultant>().ToTable("Consultants");
            // Constraints
            builder.Entity<Consultant>().HasKey(p => p.Id);
            builder.Entity<Consultant>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Consultant>().Property(p => p.UserName).IsRequired();
            builder.Entity<Consultant>().Property(p => p.FirstName).IsRequired();
            builder.Entity<Consultant>().Property(p => p.LastName).IsRequired();
            builder.Entity<Consultant>().Property(p => p.Cellphone).IsRequired();
            builder.Entity<Consultant>().Property(p => p.Email).IsRequired();
            builder.Entity<Consultant>().Property(p => p.Password).IsRequired();
            builder.Entity<Consultant>().Property(p => p.LinkedinLink).IsRequired();

            builder.Entity<Consultant>()
                .HasMany(p => p.Assignments)
                .WithOne(p => p.Consultant)
                .HasForeignKey(p => p.ConsultantId);

            builder.Entity<Consultant>()
                .HasMany(p => p.Publications)
                .WithOne(p => p.Consultant)
                .HasForeignKey(p => p.ConsultantId);

            builder.Entity<Consultant>()
                .HasMany(p => p.Appointments)
                .WithOne(p => p.Consultant)
                .HasForeignKey(p => p.ConsultantId);

            builder.Entity<Consultant>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.Consultant)
                .HasForeignKey(p => p.ConsultantId);


            builder.Entity<Consultant>().HasData
                (
                new Consultant
                { Id = 20, UserName = "abcdef" ,FirstName = "Abece", LastName = "Deefe", Cellphone = "95681914", Email = "abcdef.letras.com", Password = "password", LinkedinLink = "abcd.com" },
                new Consultant
                { Id = 22, UserName = "aeiou123",FirstName = "Aeiou", LastName = "Lol", Cellphone = "9456988", Email = "aeiou.vocal.com", Password = "P4sw0rd", LinkedinLink = "aeiou.com" }
                );

            // Publication Entity
            builder.Entity<Publication>().ToTable("Publications");

            // Constraints
            builder.Entity<Publication>().HasKey(p => p.Id);
            builder.Entity<Publication>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Publication>().Property(p => p.PublishedDate).IsRequired();
            builder.Entity<Publication>().Property(p => p.Description).IsRequired().HasMaxLength(1000);

            builder.Entity<Publication>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.Publication)
                .HasForeignKey(p => p.PublicationId);

            builder.Entity<Publication>().HasData
                (
                    new Publication
                    {
                        Id = 100,
                        PublishedDate = DateTime.Parse("2020/01/03"),
                        Description = "Mi nombre es Ana Lopez. Soy una consultora con 10 años de experiencia en el mercado.",
                        ConsultantId = 20
                    },
                    new Publication
                    {
                        Id = 101,
                        PublishedDate = DateTime.Parse("2019/12/11"),
                        Description = "Mi nombre es Luis Mamani. He trabajado por varios años como consultor de negocios en empresas reconocidas.",
                        ConsultantId = 22
                    }
                );

            // Comment Entity
            builder.Entity<Comment>().ToTable("Comments");
            builder.Entity<Comment>().HasKey(p => p.Id);
            builder.Entity<Comment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Comment>().Property(p => p.PublishedDate).IsRequired();
            builder.Entity<Comment>().Property(p => p.Description).IsRequired().HasMaxLength(500);

            builder.Entity<Comment>().HasData
                (
                    new Comment
                    {
                        Id = 200,
                        PublishedDate = DateTime.Parse("2020/01/06"),
                        Description = "La consultora Ana es muy confiable.",
                        PublicationId = 100,
                        ConsultantId=20
                    },
                    new Comment
                    {
                        Id = 201,
                        PublishedDate = DateTime.Parse("2019/12/20"),
                        Description = "Las soluciones que planteó el consultor Luis para mi negocio fueron las mejores",
                        PublicationId = 101,
                        OwnerId=1
                    }
                );

            // Appointment Entity
            builder.Entity<Appointment>().ToTable("Appointments");
            builder.Entity<Appointment>().HasKey(p => p.Id);
            builder.Entity<Appointment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Appointment>().Property(p => p.CurrentDateTime).IsRequired();
            builder.Entity<Appointment>().Property(p => p.ScheduleDateTime).IsRequired();
            builder.Entity<Appointment>().Property(p => p.Topic).IsRequired().HasMaxLength(20);
            builder.Entity<Appointment>().Property(p => p.MeetLink).IsRequired();

            builder.Entity<Appointment>()
                .HasOne(p => p.Consultancy)
                .WithOne(p => p.Appointment)
                .HasForeignKey<Consultancy>(p => p.AppointmentId);

            builder.Entity<Appointment>().HasData
                (
                    new Appointment
                    {
                        Id = 1,
                        CurrentDateTime = DateTime.Parse("2020/01/06 05:45:45"),
                        ScheduleDateTime = DateTime.Parse("2020/01/26 05:45:45"),
                        Topic = "Marketing",
                        MeetLink = "meet.com",
                        ConsultantId = 20,
                        OwnerId = 1
                    },
                    new Appointment
                    {
                        Id = 2,
                        CurrentDateTime = DateTime.Parse("2020/01/06 05:45:45"),
                        ScheduleDateTime = DateTime.Parse("2020/01/26 05:45:45"),
                        Topic = "Marketing",
                        MeetLink = "meet.com",
                        ConsultantId = 22,
                        OwnerId = 2
                    }
                );

            //Assignment Entity
            builder.Entity<Assignment>().ToTable("Assignments");
            builder.Entity<Assignment>().HasKey(p => p.Id);
            builder.Entity<Assignment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Assignment>().Property(p => p.State).IsRequired();

            builder.Entity<Assignment>().HasData
                (
                    new Assignment
                    {Id = 1, State = true, RestaurantId = 301, ConsultantId=20},
                    new Assignment
                    {Id = 2, State = false, RestaurantId = 300, ConsultantId = 22}
                );


            //Consultancy Entity
            builder.Entity<Consultancy>().ToTable("Consultancies");
            builder.Entity<Consultancy>().HasKey(p => p.Id);
            builder.Entity<Consultancy>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Consultancy>().Property(p => p.Diagnosis).IsRequired().HasMaxLength(30);
            builder.Entity<Consultancy>().Property(p => p.Recommendation).IsRequired().HasMaxLength(30);

            
            //Apply Naming Conventions Policy
            builder.ApplySnakeCaseNamingConvention();
            
        }

    }
}
