using Microsoft.AspNetCore.Mvc;
using StockMaster.Data;
using StockMaster.Models;
using StockMaster.Services;
using StockMaster.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        private readonly IPurchaseOrderService _poService;
        private readonly StockDbContext _context;

        public PurchaseOrderController(IPurchaseOrderService poService, StockDbContext context)
        {
            _poService = poService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _poService.GetAllPurchaseOrdersAsync();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
            ViewBag.Warehouses = _context.Warehouses.OrderBy(w => w.Name).ToList();
            ViewBag.Products = _context.Products.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
            return View(new PurchaseOrderCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrderCreateViewModel model)
        {
            
            if (model.Items == null || !model.Items.Any())
            {
                ModelState.AddModelError("", "Please add at least one product");
                ViewBag.Suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
                ViewBag.Warehouses = _context.Warehouses.OrderBy(w => w.Name).ToList();
                ViewBag.Products = _context.Products.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
                return View(model);
            }

            if (ModelState.IsValid || model.Items.Any())
            {
                try
                {
                    var po = new PurchaseOrder
                    {
                        SupplierId = model.SupplierId,
                        WarehouseId = model.WarehouseId,
                        ExpectedDeliveryDate = model.ExpectedDeliveryDate,
                        Status = "Pending",
                        PurchaseOrderItems = model.Items.Select(i => new PurchaseOrderItem
                        {
                            ProductId = i.ProductId,
                            Quantity = i.Quantity,
                            UnitCost = i.UnitCost
                        }).ToList()
                    };

                    var result = await _poService.CreatePurchaseOrderAsync(po);
                    if (result)
                    {
                        TempData["Success"] = "Purchase order created successfully";
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Failed to create purchase order");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

            ViewBag.Suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
            ViewBag.Warehouses = _context.Warehouses.OrderBy(w => w.Name).ToList();
            ViewBag.Products = _context.Products.Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var po = await _poService.GetPurchaseOrderByIdAsync(id);
            if (po == null)
                return NotFound();

            return View(po);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receive(int id)
        {
            try
            {
                var result = await _poService.ReceivePurchaseOrderAsync(id);
                if (result)
                {
                    TempData["Success"] = "Purchase order received and stock updated successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to receive purchase order. Order may already be received.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
            }

            return RedirectToAction("Details", new { id });
        }
    }
}