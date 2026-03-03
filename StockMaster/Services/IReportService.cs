using System.Collections.Generic;
using System.Threading.Tasks;
using StockMaster.ViewModels;

namespace StockMaster.Services
{
    public interface IReportService
    {
        Task<List<StockByWarehouseViewModel>> GetStockByWarehouseAsync();
        Task<List<ProductRevenueViewModel>> GetProductRevenueAsync();
        Task<List<POStatusViewModel>> GetPOStatusAsync();
        Task<List<CategoryRevenueViewModel>> GetCategoryRevenueAsync();
        Task<List<WarehouseCapacityViewModel>> GetWarehouseCapacityAsync();
        Task<List<StagnantProductViewModel>> GetStagnantProductsAsync();
        Task<List<StockSufficiencyViewModel>> GetStockSufficiencyAsync();
        Task<List<AnnualSalesReportViewModel>> GetAnnualSalesReportAsync();
        Task<List<DetailedPOViewModel>> GetDetailedPOReportAsync();

        Task<List<StockMaster.Models.VwSalesByDay>> GetSalesByDayAsync();
        Task<List<StockMaster.Models.VwEmployeeRanking>> GetEmployeeRankingsAsync();
        Task<StockMaster.Models.VwTodaysSummary> GetTodaysSummaryAsync();
        Task<List<StockMaster.Models.ProductPriceLog>> GetPriceLogsAsync();

        
    }
}