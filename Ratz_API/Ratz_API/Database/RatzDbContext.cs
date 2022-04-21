using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public class RatzDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<QrCode> QrCodes { get; set; } 

        public RatzDbContext(DbContextOptions<RatzDbContext> options)
            : base(options)
        {

        }
    }
}
