using System.Data.Entity;

namespace SAU.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DbConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<System> Systems { get; set; }
        public DbSet<JQLFilter> JqlFilters { get; set; }
    }
}