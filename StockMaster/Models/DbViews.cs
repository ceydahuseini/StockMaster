using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMaster.Models
{
    [Table("product_price_log", Schema = "stock_management")]
    public class ProductPriceLog
    {
        [Key]
        [Column("log_id")]
        public int LogId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("old_price")]
        public decimal OldPrice { get; set; }

        [Column("new_price")]
        public decimal NewPrice { get; set; }

        [Column("changed_by")]
        public string ChangedBy { get; set; }

        [Column("changed_at")]
        public DateTime ChangedAt { get; set; }
    }

    public class VwSalesByDay
    {
        [Column("day_number")]
        public int DayNumber { get; set; }

        [Column("day_name")]
        public string DayName { get; set; }

        [Column("total_sales")]
        public long TotalSales { get; set; }

        [Column("total_revenue")]
        public decimal TotalRevenue { get; set; }

        [Column("avg_sale_value")]
        public decimal AvgSaleValue { get; set; }

        [Column("total_items_sold")]
        public decimal TotalItemsSold { get; set; }
    }

    public class VwEmployeeRanking
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("total_sales")]
        public long TotalSales { get; set; }

        [Column("total_revenue")]
        public decimal TotalRevenue { get; set; }

        [Column("avg_sale_value")]
        public decimal AvgSaleValue { get; set; }

        [Column("unique_customers")]
        public long UniqueCustomers { get; set; }

        [Column("revenue_rank")]
        public long RevenueRank { get; set; }
    }

    
    public class VwTodaysSummary
    {
        [Column("total_transactions_today")]
        public long TotalTransactions { get; set; }

        [Column("total_revenue_today")]
        public decimal TotalRevenue { get; set; }

        [Column("total_items_sold_today")]
        public decimal TotalItemsSold { get; set; }

        [Column("unique_customers_today")]
        public long UniqueCustomers { get; set; }

        [Column("active_warehouses_today")]
        public long ActiveWarehouses { get; set; }
    }
}