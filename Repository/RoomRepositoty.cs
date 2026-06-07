using ComfortSpace.Data;
using ComfortSpace.Interfaces;
using ComfortSpace.Models;

namespace ComfortSpace.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context)
        {
            _context = context;
        }

        // Create
        public bool CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            return Save();
        }

        // Read all
        public ICollection<Room> GetRooms()
        {
            return _context.Rooms
                .OrderBy(r => r.RoomId)
                .ToList();
        }

        // Read one
        public Room GetRoom(int id)
        {
            return _context.Rooms
                .FirstOrDefault(r => r.RoomId == id);
        }

        // Exists
        public bool RoomExists(int id)
        {
            return _context.Rooms
                .Any(r => r.RoomId == id);
        }

        // Update
        public bool UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            return Save();
        }

        // Delete
        public bool DeleteRoom(Room room)
        {
            _context.Rooms.Remove(room);
            return Save();
        }

        // Save
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
