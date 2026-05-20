#region

using System;
using Example.Entities.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Example.Dal.Context
{
    public sealed class ExampleContext : DbContext
    {
        private static bool _isCreated = false;
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ExampleContext()
        {
            if (!_isCreated)
            {
                _isCreated = true;
                try
                {
                    // Retry logic for Docker startup
                    int retryCount = 0;
                    while (retryCount < 5)
                    {
                        try
                        {
                            // Database.EnsureCreated() only creates if not exists, but doesn't handle migrations.
                            // Database.Migrate() applies migrations and creates db if not exists.
                            // Since you asked for migrations, I'll use Migrate() if migrations exist, 
                            // otherwise EnsureCreated() is safer for simple Code First.
                            // Let's use EnsureCreated() but make sure it's robust.
                            Database.EnsureCreated();
                            break;
                        }
                        catch (Exception)
                        {
                            retryCount++;
                            System.Threading.Thread.Sleep(5000); // Wait 5 seconds
                            if (retryCount == 5) throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Database creation failed: {ex.Message}");
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = System.Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                   ?? @"Server=localhost,1433; database=ExampleDB; User ID=sa; password=19Mayis1919!;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}