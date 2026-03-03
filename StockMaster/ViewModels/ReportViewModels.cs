using System;

namespace StockMaster.ViewModels
{
    public class StockByWarehouseViewModel
    {
        public string WarehouseName { get; set; }
        public int TotalUnits { get; set; }
        public decimal TotalStockValue { get; set; }
    }

    public class ProductRevenueViewModel
    {
        public string ProductName { get; set; }
        public int TotalUnitsSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class POStatusViewModel
    {
        public int PoId { get; set; }
        public string Status { get; set; }
        public string ProductName { get; set; }
        public int OrderedQuantity { get; set; }
        public int ReceivedQuantity { get; set; }
        public int PendingQuantity { get; set; }
    }

    public class CategoryRevenueViewModel
    {
        public string CategoryName { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class WarehouseCapacityViewModel
    {
        public string WarehouseName { get; set; }
        public int Capacity { get; set; }
        public int UnitsInStock { get; set; }
        public double OccupancyPercentage { get; set; }
    }

    public class StagnantProductViewModel
    {
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public int QuantityOnHand { get; set; }
        public string LastSoldDate { get; set; }
    }

    public class StockSufficiencyViewModel
    {
        public string ProductName { get; set; }
        public int SoldLast60Days { get; set; }
        public double AvgDailySales { get; set; }
        public double ProjectedNext30Days { get; set; }
        public int CurrentTotalStock { get; set; }
        public string StockStatus { get; set; }
    }

    public class AnnualSalesReportViewModel
    {
        public string SalesMonth { get; set; }
        public string WarehouseName { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        public int TotalOrderCount { get; set; }
        public int TotalUnitsSold { get; set; }
        public decimal TotalGrossRevenue { get; set; }
    }

    public class DetailedPOViewModel
    {
        public int PoId { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string WarehouseName { get; set; }
        public string SupplierName { get; set; }
        public string ProductName { get; set; }
        public int OrderedQty { get; set; }
        public int ReceivedQty { get; set; }
        public int RemainingToReceive { get; set; }
    }
}