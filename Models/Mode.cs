using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("Modes")]
    public class Mode
    {
        [Key]
        [Column("mode_id")]
        public int ModeId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        public ICollection<RoomMode> RoomModes { get; set; }
    }
}
