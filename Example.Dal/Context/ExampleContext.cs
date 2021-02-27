#region

using Example.Entities.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Example.Dal.Context
{
    public sealed class ExampleContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ExampleContext()
        {
            //Hi !! I'm code firsts
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost,1433; database=ExampleDB; User ID=sa; password=19Mayis1919!;");
        }
    }
}