using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StockMaster.Models;

namespace StockMaster.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class DashboardViewModel
    {
        public List<Product> LowStockProducts { get; set; }
        public decimal TotalSalesToday { get; set; }
        public decimal TotalSalesMonth { get; set; }
        public Dictionary<string, int> StockSummary { get; set; }
    }

    public class SaleCreateViewModel
    {
        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Warehouse is required")]
        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }

        public List<SaleItemViewModel> Items { get; set; } = new();
    }

    public class SaleItemViewModel
    {
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }
    }

    public class PurchaseOrderCreateViewModel
    {
        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        [Required(ErrorMessage = "Warehouse is required")]
        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }

        [Required(ErrorMessage = "Expected delivery date is required")]
        [Display(Name = "Expected Delivery Date")]
        public DateTime ExpectedDeliveryDate { get; set; }

        public List<PurchaseOrderItemViewModel> Items { get; set; } = new();
    }

    public class PurchaseOrderItemViewModel
    {
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit cost is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit cost must be greater than 0")]
        public decimal UnitCost { get; set; }
    }
}