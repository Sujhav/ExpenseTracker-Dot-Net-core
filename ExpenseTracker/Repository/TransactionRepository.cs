using ExpenseTracker.Database;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
namespace ExpenseTracker.Repository
{
    public class TransactionRepository
    {
        private readonly ExpenseTrackerContext _context;
        public TransactionRepository(ExpenseTrackerContext expenseTrackerContext)
        {
            _context = expenseTrackerContext;
        }

        public async Task<int> AddTransactions(TransactionModel model)
        {
            var data = new Transaction()
            {
                CategoryId = model.CategoryId,
                amount = model.amount,
                notes = model.notes,
                dateTime = model.dateTime,
            };
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
            return data.TransactionId;
        }
        public async Task<List<TransactionModel>> ShowTransactions()
        {
            var model = new List<TransactionModel>();
            var data = await _context.Transactions.Include(t=>t.Category).ToListAsync();
            if (data?.Any()==true) 
            {
                foreach(var  transaction in data)
                {
                    model.Add(new TransactionModel
                    {
                        TransactionId = transaction.TransactionId,
                        CategoryId = transaction.CategoryId,
                        amount = transaction.amount,
                        notes = transaction.notes,
                        dateTime = transaction.dateTime,
                        category=transaction.Category.Icon+transaction.Category.Title,
                        formattedAmount = ((transaction.Category.Type == "Expense") ? "-" : "+") + transaction.amount.ToString("C0"),

                    });
                }
            }
            return model;
        }
        public async Task<TransactionModel?> GetSingleTransacton(int id)
        {
            var data = await _context.Transactions.FindAsync(id);
            if (data != null)
            {
                var model = new TransactionModel
                {
                    CategoryId = data.CategoryId,
                    amount = data.amount,
                    notes = data.notes,
                    dateTime = data.dateTime,
                    

                };
                return model;
            }
            return null;
            
        }
        public async Task<TransactionModel> EditTransaction(int id,TransactionModel model)
        {
            var data = await _context.Transactions.FindAsync(id);
            if(data != null)
            {
                data.notes=model.notes;
                data.dateTime=model.dateTime;
                data.amount=model.amount;
                data.CategoryId=model.CategoryId;
            }
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteTransaction(int id)
        {
            var data=  await _context.Transactions.FindAsync(id);
            if (data != null)
            {
                  _context.Transactions.Remove(data);
                  await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Transaction>> RecentTransactions()
        {
            var recent=await _context.Transactions
                .Include(i=>i.Category)
                .OrderByDescending(j=>j.dateTime)
                .Take(5)
                .ToListAsync();

            return recent;      
        }


    
    }
}
        
        
        

