using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("Stays")]
    public class Stay
    {
        [Key]
        [Column("stay_id")]
        public int StayId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("room_id")]
        public int RoomId { get; set; }

        [Column("check_in")]
        public DateTime CheckIn { get; set; }

        [Column("check_out")]
        public DateTime? CheckOut { get; set; }

        [Column("status")]
        public string Status { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}
