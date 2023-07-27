using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// // import the Entities (database models representing structure of tables in database)
using FitConnectApp.Data.Entities;
using System.Runtime.Intrinsics.X86;

namespace FitConnectApp.Data.Repositories
{
    // The Context is How EntityFramework communicates with the database
    // We define DbSet properties for each table in the database
    public class DatabaseContext : DbContext
    {
         // authentication store
        public DbSet<User> Users { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<CheckIn> CheckIn { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set;}

        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        // Configure the context with logging - remove in production
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // remove in production 
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();               
        }
          // Define primary keys and other configurations using the Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary key for User entity
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            // Configure primary key for ForgotPassword entity
            modelBuilder.Entity<ForgotPassword>().HasKey(fp => fp.Id);

            // Configure primary key for CheckIn entity
            modelBuilder.Entity<CheckIn>().HasKey(ci => ci.CheckInId);

            // Configure primary key for TrainingSession entity
            modelBuilder.Entity<TrainingSession>().HasKey(ts => ts.TrainingSessionId);

            // Configure primary key for FitnessMetrics entity
            modelBuilder.Entity<FitnessMetrics>().HasKey(fm => fm.ClientId);

            // Configure primary key for Measurements entity
            modelBuilder.Entity<Measurements>().HasKey(m => m.ClientId);

            // Add any other configurations as needed...

            base.OnModelCreating(modelBuilder);
        }

        public static DbContextOptionsBuilder<DatabaseContext> OptionsBuilder => new ();

        // Convenience method to recreate the database thus ensuring the new database takes 
        // account of any changes to Models or DatabaseContext. ONLY to be used in development
        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
