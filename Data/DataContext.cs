using ComfortSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace ComfortSpace.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Stay> Stays { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorReading> SensorReadings { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<RoomMode> RoomModes { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomMode>()
                .HasKey(rm => new { rm.RoomId, rm.ModeId });

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Stay>()
                .HasOne(s => s.User)
                .WithMany(u => u.Stays)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Stay>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Stays)
                .HasForeignKey(s => s.RoomId);

            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Sensors)
                .HasForeignKey(s => s.RoomId);

            modelBuilder.Entity<SensorReading>()
                .HasOne(sr => sr.Sensor)
                .WithMany(s => s.SensorReadings)
                .HasForeignKey(sr => sr.SensorId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);
        }
    }
}
