using ExpenseTracker.Database;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
namespace ExpenseTracker.Repository

{
    public class CategoryRepository
    {
        private readonly ExpenseTrackerContext _context = null;
        public CategoryRepository(ExpenseTrackerContext expenseTrackerContext)
        {
            _context = expenseTrackerContext;
        }

        public async Task<int> AddNewCategory(CategoryModel model)
        {
            {
                var addCategory = new Category()
                {
                    Title = model.Title,
                    Icon = model.Icon,
                    Type = model.Type,
                };
                await _context.AddAsync(addCategory);
                await _context.SaveChangesAsync();
                return addCategory.CategoryId;

            }
        }
        public async Task<List<CategoryModel>> GetCategory()
        {
            var category = new List<CategoryModel>();
            var GetCategory = await _context.Categories.ToListAsync();
            if (GetCategory?.Any() == true)
            {
                foreach (var item in GetCategory)
                {
                    category.Add(new CategoryModel
                    {
                        CategoryId=item.CategoryId,
                        Title = item.Title,
                        Icon = item.Icon,
                        Type = item.Type,
                    });;

                };

            }
            return category;
        }
        public async Task<CategoryModel> GetSingleCategory(int categoryId)
        {
            var data = await _context.Categories.FindAsync(categoryId);
            if(data!= null)
            {
                var model = new CategoryModel
                {
                    Title = data.Title,
                    Icon = data.Icon,
                    Type = data.Type,
                    CategoryId = data.CategoryId,
                };
                return model;
            }
            return null;
        }

        public async Task<CategoryModel> EditCategory(int id,CategoryModel model)
        {
            
            var Category = await _context.Categories.FirstOrDefaultAsync(x=>x.CategoryId==id);
            if(Category != null)
            {
                Category.Title = model.Title;
                Category.Icon = model.Icon;
                Category.Type = model.Type;

                await _context.SaveChangesAsync();
                return new CategoryModel();
            }
            return null;

        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category != null)
            {
                 _context.Categories.Remove(category);
                 await _context.SaveChangesAsync();
                
            }
            
        } 
    }
}
                 

                    
                

            
        
    




