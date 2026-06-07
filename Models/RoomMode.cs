using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("RoomModes")]
    public class RoomMode
    {
        [Column("room_id")]
        public int RoomId { get; set; }

        [Column("mode_id")]
        public int ModeId { get; set; }

        // Navigation
        public Room Room { get; set; }
        public Mode Mode { get; set; }


    }
}