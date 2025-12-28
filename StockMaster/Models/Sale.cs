using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMaster.Models
{
    [Table("sale", Schema = "stock_management")]
    public class Sale
    {
        [Key]
        [Column("sale_id")]
        public int SaleId { get; set; }

        [Column("date_time")]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Total amount is required")]
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column("customer_id")]
        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Warehouse is required")]
        [Column("warehouse_id")]
        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public List<SaleItem> SaleItems { get; set; }
    }
}