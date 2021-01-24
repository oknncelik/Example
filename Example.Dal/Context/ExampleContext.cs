#region

using Example.Entities.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Example.Dal.Context
{
    public class ExampleContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=OKANPC\OKANSRV; database=ExampleDB; User ID=sa; password=1234; Trusted_Connection=true");
        }
    }
}