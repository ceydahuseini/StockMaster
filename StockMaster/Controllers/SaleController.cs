using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Models;
using StockMaster.Services;
using StockMaster.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace StockMaster.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly StockDbContext _context;

        public SaleController(ISaleService saleService, StockDbContext context)
        {
            _saleService = saleService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return View(sales);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.OrderBy(c => c.Name).ToList();
            ViewBag.Warehouses = _context.Warehouses.OrderBy(w => w.Name).ToList();
            ViewBag.Products = _context.Products.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
            return View(new SaleCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleCreateViewModel model)
        {
            if (model.Items == null || !model.Items.Any())
            {
                ModelState.AddModelError("", "Please add at least one product to the sale.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = HttpContext.Session.GetInt32("UserId");
                    if (userId == null)
                    {
                        ModelState.AddModelError("", "Your session has expired. Please log in again.");
                    }
                    else
                    {
                        var sale = new Sale
                        {
                            CustomerId = model.CustomerId,
                            WarehouseId = model.WarehouseId,
                            UserId = userId,
                            TotalAmount = model.Items.Sum(i => i.Quantity * i.UnitPrice),
                            DateTime = DateTime.Now,
                            SaleItems = model.Items.Select(i => new SaleItem
                            {
                                ProductId = i.ProductId,
                                Quantity = i.Quantity,
                                UnitPriceAtSale = i.UnitPrice
                            }).ToList()
                        };

                        var result = await _saleService.CreateSaleAsync(sale);
                        if (result)
                        {
                            TempData["Success"] = "Sale created successfully!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "The sale could not be completed. Check stock levels or database rules.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    var realErrorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        realErrorMessage = ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null)
                        {
                            realErrorMessage = ex.InnerException.InnerException.Message;
                        }
                    }

                    ModelState.AddModelError("", "REAL DATABASE ERROR: " + realErrorMessage);
                }
            }

            ViewBag.Customers = _context.Customers.OrderBy(c => c.Name).ToList();
            ViewBag.Warehouses = _context.Warehouses.OrderBy(w => w.Name).ToList();
            ViewBag.Products = _context.Products.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound();
            return View(sale);
        }
    }
}
