using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        [Column("room_id")]
        public int RoomId { get; set; }

        [Column("hotel_id")]
        public int HotelId { get; set; }

        [Column("room_number")]
        [Required]
        public string RoomNumber { get; set; }

        [Column("room_type")]
        [Required]
        public string RoomType { get; set; }

        [Column("floor")]
        public int Floor { get; set; }

        // Navigation
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }

        public ICollection<Sensor> Sensors { get; set; }
        public ICollection<Stay> Stays { get; set; }
        public ICollection<RoomMode> RoomModes { get; set; }
    }
}