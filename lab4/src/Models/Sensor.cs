using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("Sensors")]
    public class Sensor
    {
        [Key]
        [Column("sensor_id")]
        public int SensorId { get; set; }

        [Column("room_id")]
        public int RoomId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("model")]
        public string? Model { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("unit")]
        public string Unit { get; set; }

        [Column("last_seen_at")]
        public DateTime? LastSeenAt { get; set; }

        [Column("status")]
        public string Status { get; set; }

        // Navigation
        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public ICollection<SensorReading> SensorReadings { get; set; }
    }
}