using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly StockDbContext _context;

        public CategoryController(StockDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Category created successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to create category");
                }
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Category updated successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to update category");
                }
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Category deleted successfully";
                }
            }
            catch
            {
                TempData["Error"] = "Cannot delete category. It may be in use.";
            }
            return RedirectToAction("Index");
        }
    }
}