using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Models;
using StockMaster.Services;
using System.Linq;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly StockDbContext _context;

        public ProductController(IProductService productService, StockDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            
            ModelState.Remove("Category");
            ModelState.Remove("Supplier");

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.CreateProductAsync(product);
                    if (result)
                    {
                        TempData["Success"] = "Product created successfully";
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Failed to create product. SKU may already exist.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Failed to create product: {ex.Message}");
                }
            }

            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            
            ModelState.Remove("Category");
            ModelState.Remove("Supplier");

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.UpdateProductAsync(product);
                    if (result)
                    {
                        TempData["Success"] = "Product updated successfully";
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Failed to update product");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Failed to update product: {ex.Message}");
                }
            }

            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result)
                {
                    TempData["Success"] = "Product deleted successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to delete product";
                }
            }
            catch
            {
                TempData["Error"] = "Cannot delete product. It may be in use.";
            }
            return RedirectToAction("Index");
        }
    }
}