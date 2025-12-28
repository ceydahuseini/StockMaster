using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    public class WarehouseController : BaseController
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
            var warehouses = await _context.Warehouses.ToListAsync();
            return View(warehouses);
        }

        public async Task<IActionResult> Stock(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
                return NotFound();

            var stock = await _warehouseService.GetWarehouseStockAsync(id);
            ViewBag.Warehouse = warehouse;
            return View(stock);
        }
    }
}