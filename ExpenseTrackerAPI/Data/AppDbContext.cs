using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Models.Domain;

namespace ExpenseTrackerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
