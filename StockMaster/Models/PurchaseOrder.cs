using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMaster.Models
{
    [Table("purchase_order", Schema = "stock_management")]
    public class PurchaseOrder
    {
        [Key]
        [Column("po_id")]
        public int PoId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "Expected delivery date is required")]
        [Column("expected_delivery_date")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [Column("actual_delivery_date")]
        public DateTime? ActualDeliveryDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [MaxLength(20)]
        [Column("status")]
        public string Status { get; set; }

        [Column("supplier_id")]
        public int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        [Required(ErrorMessage = "Warehouse is required")]
        [Column("warehouse_id")]
        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}