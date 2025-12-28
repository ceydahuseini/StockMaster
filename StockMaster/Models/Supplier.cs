using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMaster.Models
{
    [Table("supplier", Schema = "stock_management")]
    public class Supplier
    {
        [Key]
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier name is required")]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact person is required")]
        [MaxLength(100)]
        [Column("contact_person")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Column("address")]
        public string Address { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}