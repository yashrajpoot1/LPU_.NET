using Ecommerce_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Inventory.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
