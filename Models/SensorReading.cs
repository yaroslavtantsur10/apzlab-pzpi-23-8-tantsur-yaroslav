using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComfortSpace.Models
{
    [Table("SensorReadings")]
    public class SensorReading
    {
        [Key]
        [Column("reading_id")]
        public long ReadingId { get; set; }

        [Column("sensor_id")]
        public int SensorId { get; set; }

        [Column("value")]
        public decimal Value { get; set; }

        [Column("captured_at")]
        public DateTime CapturedAt { get; set; }

        // Navigation
        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; }
    }
}