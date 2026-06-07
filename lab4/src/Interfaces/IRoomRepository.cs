using ComfortSpace.Models;

namespace ComfortSpace.Interfaces
{
    public interface IRoomRepository
    {
        bool CreateRoom(Room room);

        ICollection<Room> GetRooms();
        Room GetRoom(int id);
        bool RoomExists(int id);
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);

        bool Save();
    }
}
