using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMaster.Models
{
    [Table("customer", Schema = "stock_management")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Column("address")]
        public string Address { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}