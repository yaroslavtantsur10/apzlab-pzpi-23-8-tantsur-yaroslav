using ComfortSpace.Models;

namespace ComfortSpace.Interfaces
{
    public interface IHotelRepository
    {
        bool CreateHotel(Hotel hotel);

        ICollection<Hotel> GetHotels();
        Hotel GetHotel(int id);

        bool HotelExists(int id);

        bool UpdateHotel(Hotel hotel);
        bool DeleteHotel(Hotel hotel);

        bool Save();
    }
}
