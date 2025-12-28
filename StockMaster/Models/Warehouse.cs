using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMaster.Models
{
    [Table("warehouse", Schema = "stock_management")]
    public class Warehouse
    {
        [Key]
        [Column("warehouse_id")]
        public int WarehouseId { get; set; }

        [Required(ErrorMessage = "Warehouse name is required")]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [MaxLength(255)]
        [Column("location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        [Column("capacity")]
        public int Capacity { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}