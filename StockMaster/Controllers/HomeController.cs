using Microsoft.AspNetCore.Mvc;
using StockMaster.Services;
using StockMaster.ViewModels;
using System;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ISaleService _saleService;
        private readonly IWarehouseService _warehouseService;

        public HomeController(
            IProductService productService,
            ISaleService saleService,
            IWarehouseService warehouseService)
        {
            _productService = productService;
            _saleService = saleService;
            _warehouseService = warehouseService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                LowStockProducts = await _productService.GetLowStockProductsAsync(),
                TotalSalesToday = await _saleService.GetTotalSalesAmountAsync(
                    DateTime.Today,
                    DateTime.Today.AddDays(1)),
                TotalSalesMonth = await _saleService.GetTotalSalesAmountAsync(
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)),
                StockSummary = await _warehouseService.GetStockSummaryAsync()
            };

            return View(viewModel);
        }
    }
}