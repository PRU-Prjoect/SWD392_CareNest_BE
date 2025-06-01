using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Sub_Address> Sub_Address { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<ImageGallery> ImageGallery { get; set; }
        public DbSet<Pet_Type> Pet_Type { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Service_Appointment> Service_Appointment { get; set; }
        public DbSet<Service_Type> Service_Type { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Account>().HasIndex(a => a.email).IsUnique();
            builder.Entity<Account>().HasIndex(a => a.username).IsUnique();

            builder.Entity<Shop>().Property(a => a.working_day).HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>(),
                new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                    )
            );
        }
    }
}
