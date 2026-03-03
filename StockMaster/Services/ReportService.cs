using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMaster.Data;
using StockMaster.ViewModels;

namespace StockMaster.Services
{
    public class ReportService : IReportService
    {
        private readonly StockDbContext _context;

        public ReportService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockByWarehouseViewModel>> GetStockByWarehouseAsync()
        {
            return await _context.WarehouseStocks
                .Include(ws => ws.Warehouse)
                .Include(ws => ws.Product)
                .GroupBy(ws => new { ws.Warehouse.WarehouseId, ws.Warehouse.Name })
                .Select(g => new StockByWarehouseViewModel
                {
                    WarehouseName = g.Key.Name,
                    TotalUnits = g.Sum(ws => ws.QuantityOnHand),
                    TotalStockValue = g.Sum(ws => ws.QuantityOnHand * ws.Product.UnitPrice)
                })
                .OrderByDescending(r => r.TotalStockValue)
                .ToListAsync();
        }

        public async Task<List<ProductRevenueViewModel>> GetProductRevenueAsync()
        {
            return await _context.SaleItems
                .Include(si => si.Product)
                .GroupBy(si => new { si.Product.ProductId, si.Product.Name })
                .Select(g => new ProductRevenueViewModel
                {
                    ProductName = g.Key.Name,
                    TotalUnitsSold = g.Sum(si => si.Quantity),
                    TotalRevenue = g.Sum(si => si.Quantity * si.UnitPriceAtSale)
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToListAsync();
        }

        public async Task<List<POStatusViewModel>> GetPOStatusAsync()
        {
            return await _context.PurchaseOrderItems
                .Include(poi => poi.PurchaseOrder)
                .Include(poi => poi.Product)
                .Select(poi => new POStatusViewModel
                {
                    PoId = poi.PoId,
                    Status = poi.PurchaseOrder.Status,
                    ProductName = poi.Product.Name,
                    OrderedQuantity = poi.Quantity,
                    ReceivedQuantity = poi.ReceivedQuantity,
                    PendingQuantity = poi.Quantity - poi.ReceivedQuantity
                })
                .OrderBy(r => r.PoId)
                .ToListAsync();
        }

        public async Task<List<CategoryRevenueViewModel>> GetCategoryRevenueAsync()
        {
            return await _context.SaleItems
                .Include(si => si.Product)
                .ThenInclude(p => p.Category)
                .GroupBy(si => new { si.Product.Category.CategoryId, si.Product.Category.Name })
                .Select(g => new CategoryRevenueViewModel
                {
                    CategoryName = g.Key.Name,
                    TotalRevenue = g.Sum(si => si.Quantity * si.UnitPriceAtSale)
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToListAsync();
        }

        public async Task<List<WarehouseCapacityViewModel>> GetWarehouseCapacityAsync()
        {
            var warehouses = await _context.Warehouses.ToListAsync();
            var stocks = await _context.WarehouseStocks.ToListAsync();

            var result = new List<WarehouseCapacityViewModel>();

            foreach (var w in warehouses)
            {
                var currentStock = stocks.Where(ws => ws.WarehouseId == w.WarehouseId).Sum(ws => ws.QuantityOnHand);
                double percentage = w.Capacity > 0 ? ((double)currentStock / w.Capacity) * 100 : 0;

                result.Add(new WarehouseCapacityViewModel
                {
                    WarehouseName = w.Name,
                    Capacity = w.Capacity,
                    UnitsInStock = currentStock,
                    OccupancyPercentage = Math.Round(percentage, 2)
                });
            }
            return result;
        }

        public async Task<List<StagnantProductViewModel>> GetStagnantProductsAsync()
        {
            var ninetyDaysAgo = DateTime.Now.AddDays(-90);
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            var stocks = await _context.WarehouseStocks.ToListAsync();

            var recentSales = await _context.SaleItems
                .Include(si => si.Sale)
                .Where(si => si.Sale.DateTime >= ninetyDaysAgo)
                .Select(si => si.ProductId)
                .Distinct()
                .ToListAsync();

            var result = new List<StagnantProductViewModel>();

            foreach (var p in products)
            {
                if (!recentSales.Contains(p.ProductId))
                {
                    var totalStock = stocks.Where(ws => ws.ProductId == p.ProductId).Sum(ws => ws.QuantityOnHand);
                    if (totalStock > 0)
                    {
                        result.Add(new StagnantProductViewModel
                        {
                            ProductName = p.Name,
                            Sku = p.Sku,
                            QuantityOnHand = totalStock,
                            LastSoldDate = "No sales in 90 days"
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<StockSufficiencyViewModel>> GetStockSufficiencyAsync()
        {
            var sixtyDaysAgo = DateTime.Now.AddDays(-60);

            var recentSales = await _context.SaleItems
                .Include(si => si.Sale)
                .Where(si => si.Sale.DateTime >= sixtyDaysAgo)
                .ToListAsync();

            var stocks = await _context.WarehouseStocks.ToListAsync();
            var products = await _context.Products.Where(p => p.IsActive).ToListAsync();

            var report = new List<StockSufficiencyViewModel>();

            foreach (var p in products)
            {
                var sold = recentSales.Where(x => x.ProductId == p.ProductId).Sum(x => x.Quantity);
                var currentStock = stocks.Where(x => x.ProductId == p.ProductId).Sum(x => x.QuantityOnHand);

                double avgDaily = (double)sold / 60.0;
                double projected30 = avgDaily * 30.0;

                report.Add(new StockSufficiencyViewModel
                {
                    ProductName = p.Name,
                    SoldLast60Days = sold,
                    AvgDailySales = Math.Round(avgDaily, 2),
                    ProjectedNext30Days = Math.Round(projected30, 2),
                    CurrentTotalStock = currentStock,
                    StockStatus = currentStock >= projected30 ? "SUFFICIENT" : "INSUFFICIENT"
                });
            }

            return report.OrderBy(r => r.StockStatus).ThenByDescending(r => r.ProjectedNext30Days).ToList();
        }

        public async Task<List<AnnualSalesReportViewModel>> GetAnnualSalesReportAsync()
        {
            var twelveMonthsAgo = DateTime.Now.AddMonths(-11).Date;

            var rawData = await _context.Sales
                .Include(s => s.Warehouse)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                    .ThenInclude(p => p.Category)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                    .ThenInclude(p => p.Supplier)
                .Where(s => s.DateTime >= twelveMonthsAgo)
                .SelectMany(s => s.SaleItems.Select(si => new
                {
                    SaleId = s.SaleId,
                    Date = s.DateTime,
                    WarehouseName = s.Warehouse.Name,
                    CategoryName = si.Product.Category != null ? si.Product.Category.Name : "Uncategorized",
                    SupplierName = si.Product.Supplier != null ? si.Product.Supplier.Name : "Unknown",
                    Quantity = si.Quantity,
                    Revenue = si.Quantity * si.UnitPriceAtSale
                }))
                .ToListAsync();

            var groupedData = rawData
                .GroupBy(x => new
                {
                    Month = x.Date.ToString("yyyy-MM"),
                    x.WarehouseName,
                    x.CategoryName,
                    x.SupplierName
                })
                .Select(g => new AnnualSalesReportViewModel
                {
                    SalesMonth = g.Key.Month,
                    WarehouseName = g.Key.WarehouseName,
                    CategoryName = g.Key.CategoryName,
                    SupplierName = g.Key.SupplierName,
                    TotalOrderCount = g.Select(x => x.SaleId).Distinct().Count(),
                    TotalUnitsSold = g.Sum(x => x.Quantity),
                    TotalGrossRevenue = g.Sum(x => x.Revenue)
                })
                .OrderByDescending(x => x.SalesMonth)
                .ThenByDescending(x => x.TotalGrossRevenue)
                .ToList();

            return groupedData;
        }

        public async Task<List<DetailedPOViewModel>> GetDetailedPOReportAsync()
        {
            var query = await _context.PurchaseOrders
                .Include(po => po.PurchaseOrderItems)
                    .ThenInclude(poi => poi.Product)
                .Include(po => po.Supplier)
                .Include(po => po.Warehouse)
                .ToListAsync();

            var result = query.SelectMany(po => po.PurchaseOrderItems.Select(poi => new DetailedPOViewModel
            {
                PoId = po.PoId,
                Status = po.Status,
                OrderDate = po.OrderDate,
                ExpectedDeliveryDate = po.ExpectedDeliveryDate,
                WarehouseName = po.Warehouse.Name,
                SupplierName = po.Supplier != null ? po.Supplier.Name : "Unknown",
                ProductName = poi.Product.Name,
                OrderedQty = poi.Quantity,
                ReceivedQty = poi.ReceivedQuantity,
                RemainingToReceive = poi.Quantity - poi.ReceivedQuantity
            }))
            .OrderBy(x => x.Status)
            .ThenBy(x => x.ExpectedDeliveryDate)
            .ToList();

            return result;
        }

        public async Task<List<StockMaster.Models.VwSalesByDay>> GetSalesByDayAsync()
        {
            return await _context.VwSalesByDays.ToListAsync();
        }

        public async Task<List<StockMaster.Models.VwEmployeeRanking>> GetEmployeeRankingsAsync()
        {
            return await _context.VwEmployeeRankings.ToListAsync();
        }

        public async Task<StockMaster.Models.VwTodaysSummary> GetTodaysSummaryAsync()
        {
            return await _context.VwTodaysSummaries.FirstOrDefaultAsync() ?? new StockMaster.Models.VwTodaysSummary();
        }

        public async Task<List<StockMaster.Models.ProductPriceLog>> GetPriceLogsAsync()
        {
            return await _context.ProductPriceLogs.OrderByDescending(l => l.ChangedAt).ToListAsync();
        }

    }
}