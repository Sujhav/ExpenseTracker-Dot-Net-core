using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Database
{
    public class ExpenseTrackerContext:DbContext
    {
        public ExpenseTrackerContext(DbContextOptions <ExpenseTrackerContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
