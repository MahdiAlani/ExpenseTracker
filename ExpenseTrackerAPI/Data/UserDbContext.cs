using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Models.Domain;

namespace ExpenseTrackerAPI.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
