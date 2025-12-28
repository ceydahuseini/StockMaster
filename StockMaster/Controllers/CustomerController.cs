using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly StockDbContext _context;

        public CustomerController(StockDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
                .OrderBy(c => c.Name)
                .ToListAsync();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Customer created successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to create customer");
                }
            }
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Customer updated successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to update customer");
                }
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Customer deleted successfully";
                }
            }
            catch
            {
                TempData["Error"] = "Cannot delete customer. It may be in use.";
            }
            return RedirectToAction("Index");
        }
    }
}