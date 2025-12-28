using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMaster.Models
{
    [Table("purchase_order_item", Schema = "stock_management")]
    public class PurchaseOrderItem
    {
        [Column("po_id")]
        public int PoId { get; set; }

        [ForeignKey("PoId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit cost is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit cost must be greater than 0")]
        [Column("unit_cost")]
        public decimal UnitCost { get; set; }

        [Column("received_quantity")]
        public int ReceivedQuantity { get; set; } = 0;
    }
}