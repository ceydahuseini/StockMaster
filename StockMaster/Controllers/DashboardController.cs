using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMaster.Data;

namespace StockMaster.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly StockDbContext _context;
        public DashboardController(StockDbContext context) => _context = context;

        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.Count();
            ViewBag.TodaySales = _context.Sales.Where(s => s.DateTime.Date == DateTime.Today).Sum(s => s.TotalAmount);
            return View();
        }
    }
}