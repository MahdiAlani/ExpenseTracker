using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Models.Domain;

namespace ExpenseTrackerAPI.Data
{
    public class ReceiptDbContext : DbContext
    {
        public ReceiptDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Receipt> Receipts { get; set; }
    }
}
