using ECommerceAPI.Core.Entities;
using System.Data.Entity;

namespace ECommerceAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection") // Connection string name from Web.config
        {
        }

        // DbSets (Tables)
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        // Add more later (Users, Orders, etc.)
    }
}
