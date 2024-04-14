using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class CategoryModel
    {
        
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Title is Required")]
        
        public string Title { get; set; }

        [Required]
        public string Icon { get; set; } = "";

        [Required]
        public string Type { get; set; } = "Expense";

        public string TitleWithIcon
        {
            get
            {
                return this.Icon + " " + this.Title;
            }
        }
      

    }
}
