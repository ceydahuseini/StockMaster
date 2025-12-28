using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly StockDbContext _context;

        public SupplierController(StockDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await _context.Suppliers
                .OrderBy(s => s.Name)
                .ToListAsync();
            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Suppliers.Add(supplier);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Supplier created successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to create supplier");
                }
            }
            return View(supplier);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Suppliers.Update(supplier);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Supplier updated successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to update supplier");
                }
            }
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier != null)
                {
                    _context.Suppliers.Remove(supplier);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Supplier deleted successfully";
                }
            }
            catch
            {
                TempData["Error"] = "Cannot delete supplier. It may be in use.";
            }
            return RedirectToAction("Index");
        }
    }
}