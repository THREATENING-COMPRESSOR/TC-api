using TC.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

// Specifies the namespace in which the class is declared
namespace TC.Data
{
    // The 'StoreContext' class serves as a data context for database operations
    public class StoreContext : DbContext
    {
        // Constructor for the 'StoreContext' class that takes in 'DbContextOptions'
        public StoreContext(DbContextOptions<StoreContext> options)
            // Calls the base class constructor with the provided 'options'
            : base(options)
        { }

        // Represents a database table 'Item' in the context
        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }
        // 

        // override bases class's
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DbInitializer.Initialize(builder);
        }
    }
}


