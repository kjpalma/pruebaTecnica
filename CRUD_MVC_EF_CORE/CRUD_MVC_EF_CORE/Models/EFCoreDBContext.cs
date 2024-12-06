using Microsoft.EntityFrameworkCore;

namespace CRUD_MVC_EF_CORE.Models
{
    public class EFCoreDBContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public EFCoreDBContext(DbContextOptions<EFCoreDBContext> options) : base(options)
        {
        }

        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //Adding Domain Classes as DbSet Properties
        public DbSet<Product> Products { get; set; }
    }
}
