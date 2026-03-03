using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Models;
using StockMaster.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace StockMaster.Controllers
{
    [Authorize]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;
        private readonly StockDbContext _context;

        public WarehouseController(IWarehouseService warehouseService, StockDbContext context)
        {
            _warehouseService = warehouseService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var warehouses = await _context.Warehouses.OrderBy(w => w.Name).ToListAsync();
            return View(warehouses);
        }

        public async Task<IActionResult> Stock(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null) return NotFound();

            var stock = await _warehouseService.GetWarehouseStockAsync(id);
            ViewBag.Warehouse = warehouse;
            return View(stock);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Warehouses.Add(warehouse);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Warehouse created successfully.";
                return RedirectToAction("Index");
            }
            return View(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null) return NotFound();
            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Warehouses.Update(warehouse);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Warehouse updated successfully.";
                return RedirectToAction("Index");
            }
            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var warehouse = await _context.Warehouses.FindAsync(id);
                if (warehouse != null)
                {
                    _context.Warehouses.Remove(warehouse);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Warehouse deleted successfully.";
                }
            }
            catch
            {
                TempData["Error"] = "Cannot delete warehouse. It contains stock or historical data.";
            }
            return RedirectToAction("Index");
        }
    }
}