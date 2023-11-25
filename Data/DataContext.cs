using Microsoft.EntityFrameworkCore;

namespace _11112023ClassWork.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }
        public DataContext(DbContextOptions options) : base(options) { }
    }
}
