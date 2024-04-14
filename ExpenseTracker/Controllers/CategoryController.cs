using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using ExpenseTracker.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;
        public CategoryController(CategoryRepository categoryRepository)
        {
            _repository = categoryRepository;
        }
        [HttpGet]
        public  IActionResult Create()
        {
            return View(new CategoryModel());
           
        }

        [HttpPost]
        public async Task< IActionResult> Create(CategoryModel model)
        
        {
            if (ModelState.IsValid)
            {
                int id = await _repository.AddNewCategory(model);
                if (id > 0)
                {
                    return RedirectToAction("Create");
                }
               
            }
            
            return View(model);
        }

        [Route("Category")]
        public async Task<IActionResult> ShowCategory()
        {

            var Category=await _repository.GetCategory();
            if(Category!=null)
            {
                return View(Category);
            }

            return View();
        }
        [Route("~/EditCategories/{id}")]
        
        public async Task<IActionResult> EditCategory(int id)
        {
            var data = await _repository.GetSingleCategory(id);
            return View(data);
        }

        [Route("~/EditCategories/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditCategory(int id,CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _repository.EditCategory(id, model);
                ModelState.Clear();
                return View(data);
            }
            return View(model) ;
        }
        [HttpPost]
        [Route("~/DeleteCategories/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory( int id)
        {
             await _repository.DeleteCategory(id);
            return RedirectToAction("Create", "Category");
        }
    }
}
