using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockMaster.Services;
using System.Threading.Tasks;

namespace StockMaster.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StockByWarehouse()
        {
            var data = await _reportService.GetStockByWarehouseAsync();
            return View(data);
        }

        public async Task<IActionResult> ProductRevenue()
        {
            var data = await _reportService.GetProductRevenueAsync();
            return View(data);
        }

        public async Task<IActionResult> POStatus()
        {
            var data = await _reportService.GetPOStatusAsync();
            return View(data);
        }

        public async Task<IActionResult> CategoryRevenue()
        {
            var data = await _reportService.GetCategoryRevenueAsync();
            return View(data);
        }

        public async Task<IActionResult> WarehouseCapacity()
        {
            var data = await _reportService.GetWarehouseCapacityAsync();
            return View(data);
        }

        public async Task<IActionResult> StagnantProducts()
        {
            var data = await _reportService.GetStagnantProductsAsync();
            return View(data);
        }

        public async Task<IActionResult> StockSufficiency()
        {
            var data = await _reportService.GetStockSufficiencyAsync();
            return View(data);
        }

        public async Task<IActionResult> AnnualSales()
        {
            var data = await _reportService.GetAnnualSalesReportAsync();
            return View(data);
        }

        public async Task<IActionResult> DetailedPO()
        {
            var data = await _reportService.GetDetailedPOReportAsync();
            return View(data);
        }

        public async Task<IActionResult> PriceLogs()
        {
            var data = await _reportService.GetPriceLogsAsync();
            return View(data);
        }

        public async Task<IActionResult> ExportStockByWarehouse()
        {
            var data = await _reportService.GetStockByWarehouseAsync();
            return CreateCsvFile(data, "StockByWarehouse.csv");
        }

        public async Task<IActionResult> ExportProductRevenue()
        {
            var data = await _reportService.GetProductRevenueAsync();
            return CreateCsvFile(data, "ProductRevenue.csv");
        }

        public async Task<IActionResult> ExportPOStatus()
        {
            var data = await _reportService.GetPOStatusAsync();
            return CreateCsvFile(data, "POStatus.csv");
        }

        public async Task<IActionResult> ExportCategoryRevenue()
        {
            var data = await _reportService.GetCategoryRevenueAsync();
            return CreateCsvFile(data, "CategoryRevenue.csv");
        }

        public async Task<IActionResult> ExportWarehouseCapacity()
        {
            var data = await _reportService.GetWarehouseCapacityAsync();
            return CreateCsvFile(data, "WarehouseCapacity.csv");
        }

        public async Task<IActionResult> ExportStagnantProducts()
        {
            var data = await _reportService.GetStagnantProductsAsync();
            return CreateCsvFile(data, "StagnantProducts.csv");
        }

        public async Task<IActionResult> ExportStockSufficiency()
        {
            var data = await _reportService.GetStockSufficiencyAsync();
            return CreateCsvFile(data, "StockTrendAnalysis.csv");
        }

        public async Task<IActionResult> ExportAnnualSales()
        {
            var data = await _reportService.GetAnnualSalesReportAsync();
            return CreateCsvFile(data, "AnnualSales.csv");
        }

        public async Task<IActionResult> ExportDetailedPO()
        {
            var data = await _reportService.GetDetailedPOReportAsync();
            return CreateCsvFile(data, "DetailedPO.csv");
        }

        public async Task<IActionResult> SalesByDay()
        {
            var data = await _reportService.GetSalesByDayAsync();

            return View(data);
        }

        public async Task<IActionResult> ExportSalesByDay()
        {
            var data = await _reportService.GetSalesByDayAsync();
            return CreateCsvFile(data, "SalesByDay.csv");
        }

        public async Task<IActionResult> ExportPriceLogs()
        {
            var data = await _reportService.GetPriceLogsAsync();
            return CreateCsvFile(data, "PriceChangeLogs.csv");
        }

        public async Task<IActionResult> ExportEmployeeRanking()
        {
            var data = await _reportService.GetEmployeeRankingsAsync();
            return CreateCsvFile(data, "EmployeePerformanceReport.csv");
        }

        public async Task<IActionResult> EmployeePerformance()
        {
            var data = await _reportService.GetEmployeeRankingsAsync();
            return View(data);
        }

        public async Task<IActionResult> TodaysSummary()
        {
            var data = await _reportService.GetTodaysSummaryAsync();
            return View(data);
        }

        public async Task<IActionResult> ExportTodaysSummary()
        {
            var data = await _reportService.GetTodaysSummaryAsync();
            return CreateCsvFile(new List<StockMaster.Models.VwTodaysSummary> { data }, "TodaysSummary.csv");
        }

        private FileResult CreateCsvFile<T>(IEnumerable<T> data, string fileName)
        {
            var builder = new System.Text.StringBuilder();
            var props = typeof(T).GetProperties();

            builder.AppendLine(string.Join(",", props.Select(p => p.Name)));

            foreach (var item in data)
            {
                var values = new List<string>();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(item, null) ?? "";
                    var strValue = value.ToString().Replace("\"", "\"\""); 

                    
                    if (strValue.Contains(",") || strValue.Contains("\n"))
                    {
                        strValue = $"\"{strValue}\"";
                    }
                    values.Add(strValue);
                }
                builder.AppendLine(string.Join(",", values));
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(builder.ToString());
            return File(bytes, "text/csv", fileName);
        }
    }



}