using Example.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Dal.Context
{
    public class ExampleContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=OKANPC\OKANSRV; database=ExampleDB; User ID=sa; password=1234; Trusted_Connection=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
