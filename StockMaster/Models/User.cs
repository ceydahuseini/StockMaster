using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMaster.Models
{
    [Table("users", Schema = "stock_management")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50)]
        [Column("username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(255)]
        [Column("password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100)]
        [Column("full_name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [MaxLength(20)]
        [Column("role")]
        public string Role { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}