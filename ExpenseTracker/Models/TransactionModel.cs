using ExpenseTracker.Database;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace ExpenseTracker.Models
{
    public class TransactionModel
    {

        public int TransactionId { get; set; }

        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Select a category.")]
        public int CategoryId { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Amount should be greater than 0.")]
        public int amount { get; set; }


        public string notes { get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;

        [ValidateNever]
        public string category {  get; set; }

        [ValidateNever]
        public string formattedAmount {  get; set; }

        //[NotMapped]
        
    }
}
