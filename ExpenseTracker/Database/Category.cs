using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Database
{
    public class Category
    {
        
        public int CategoryId { get; set; }

        public string Title { get; set; }


        public string Icon { get; set; } = "";


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
