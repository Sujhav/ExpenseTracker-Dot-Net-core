using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using ExpenseTracker.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracker.Database;
using Microsoft.EntityFrameworkCore;
namespace ExpenseTracker.Controllers
{
    
    public class TransactionsController : Controller
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly ExpenseTrackerContext _context;
        public TransactionsController(TransactionRepository transactionRepository,ExpenseTrackerContext expenseTrackerContext)
        {
            _transactionRepository = transactionRepository; 
            _context = expenseTrackerContext;

        }
         
        [HttpGet]
        public  IActionResult AddTransactions()
        {
            
            populate();
            return View(new TransactionModel());
        }
        [HttpPost]
        public async Task< IActionResult> AddTransactions(TransactionModel model)
        {
            if (ModelState.IsValid)
            {
                var id = await _transactionRepository.AddTransactions(model);
                if (id > 0)
                {
                    return RedirectToAction("AddTransactions");
                }
            }
            populate();
            return View(model);
        }
        [Route("Transactions")]
        public async Task<IActionResult> ShowAllTransactios()
        {
            var data= await _transactionRepository.ShowTransactions();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> EditTransaction(int id)
        {
            var data=await _transactionRepository.GetSingleTransacton(id);
            populate();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditTransaction(int id,TransactionModel model)
        {
            if(ModelState.IsValid)
            {
                var data = await _transactionRepository.EditTransaction(id, model);
                if (data != null)
                {
                    ModelState.Clear();
                    return RedirectToAction("ShowAllTransactios");
                }

            }
            return View(model);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTranscation(int id)
        {
            await _transactionRepository.DeleteTransaction(id);
            return RedirectToAction("ShowAllTransactios");
        }

        [NonAction]
        public void populate()
        {
            var category = _context.Categories.ToList();
            Category defaultCategory = new Category() { CategoryId = 0, Title = "select  a category"  };
            category.Insert(0, defaultCategory);
            ViewBag.category=category;

        }       
    }


}

