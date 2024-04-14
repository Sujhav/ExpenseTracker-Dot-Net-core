using ExpenseTracker.Database;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ExpenseTracker.Repository

{
    public class DashboardRepository
    {
        private readonly ExpenseTrackerContext _context;
        public DashboardRepository(ExpenseTrackerContext context)
        {
            _context = context;
        }
         
        public async Task<List<Transaction>> Datetime()
        {
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime Enddate = DateTime.Today;
            List<Transaction> SelectedTransaction = await _context.Transactions
                .Include(x=>x.Category)
                .Where(y=>y.dateTime>=StartDate&&y.dateTime<=Enddate)
                .ToListAsync();

                return SelectedTransaction; 
        }

        public async Task<TransactionSummaryModel> Amount()
        {
            List<Transaction> SelectedTransaction= await Datetime();
            int TotalIncome=SelectedTransaction.Where(i=>i.Category.Type=="Income")
                .Sum(j=>j.amount);
            

            int TotalExpense=SelectedTransaction.Where(i=>i.Category.Type=="Expense")
                .Sum(j=>j.amount);

            int Balance = TotalIncome - TotalExpense;

            return new TransactionSummaryModel
            {
                Expense = TotalExpense,
                Income = TotalIncome,
                Balance = Balance
            };
        }
    }
}
