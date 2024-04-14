using ExpenseTracker.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Database
{
    public class Transaction
    {
     
        public int TransactionId { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int amount { get; set; }

        public string notes { get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;

        [NotMapped]
        public string formattedAmount
        {
            get
            {
                return ((Category.Type == "Expense") ? "-" : "+") + amount.ToString("C0");
            }
        }


    }
}

