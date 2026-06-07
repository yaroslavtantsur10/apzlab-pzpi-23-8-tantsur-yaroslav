using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("surname")]
        [Required]
        public string Surname { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("patronymic")]
        public string? Patronymic { get; set; }

        [Column("email")]
        [Required]
        public string Email { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("status")]
        [Required]
        public string Status { get; set; }

        [Column("role")]
        [Required]
        public string Role { get; set; }

        // Navigation
        public ICollection<Stay> Stays { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}