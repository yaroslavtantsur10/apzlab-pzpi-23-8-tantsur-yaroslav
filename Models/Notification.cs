using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        [Column("notification_id")]
        public long NotificationId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}