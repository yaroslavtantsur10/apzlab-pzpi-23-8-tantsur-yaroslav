using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("Hotels")]
    public class Hotel
    {
        [Key]
        [Column("hotel_id")]
        public int HotelId { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("country")]
        [Required]
        public string Country { get; set; }

        [Column("city")]
        [Required]
        public string City { get; set; }

        [Column("street")]
        [Required]
        public string Street { get; set; }

        [Column("building_number")]
        [Required]
        public string BuildingNumber { get; set; }

        // Navigation
        public ICollection<Room> Rooms { get; set; }
    }
}