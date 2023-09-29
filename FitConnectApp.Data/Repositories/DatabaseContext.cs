using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// // import the Entities (database models representing structure of tables in database)
using FitConnectApp.Data.Entities;
using System.Runtime.Intrinsics.X86;

namespace FitConnectApp.Data.Repositories
{
    public class DatabaseContext : DbContext
    {
         // authentication store
        public DbSet<User> Users { get; set; }
        public DbSet<HealthData> HealthData {get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<CheckIn> CheckIn { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }

        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();               
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary key for User entity
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            // Configure primary key for ForgotPassword entity
            modelBuilder.Entity<ForgotPassword>().HasKey(fp => fp.Id);
            
            modelBuilder.Entity<User>()
            .Property(e => e.Role)
            .HasConversion<string>(); 

            base.OnModelCreating(modelBuilder);
        }

        public static DbContextOptionsBuilder<DatabaseContext> OptionsBuilder => new ();

        public void Initialise()
        {
             Database.EnsureDeleted();
             Database.EnsureCreated();
        }

    }
}
