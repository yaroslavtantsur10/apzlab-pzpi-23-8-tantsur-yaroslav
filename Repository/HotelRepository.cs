using ComfortSpace.Data;
using ComfortSpace.Interfaces;
using ComfortSpace.Models;

namespace ComfortSpace.Repository
{
        public class HotelRepository : IHotelRepository
        {
            private readonly DataContext _context;

            public HotelRepository(DataContext context)
            {
                _context = context;
            }

            public bool CreateHotel(Hotel hotel)
            {
                _context.Hotels.Add(hotel);
                return Save();
            }

            public ICollection<Hotel> GetHotels()
            {
                return _context.Hotels
                    .OrderBy(h => h.HotelId)
                    .ToList();
            }

            public Hotel GetHotel(int id)
            {
                return _context.Hotels
                    .FirstOrDefault(h => h.HotelId == id);
            }

            public bool HotelExists(int id)
            {
                return _context.Hotels
                    .Any(h => h.HotelId == id);
            }

            public bool UpdateHotel(Hotel hotel)
            {
                _context.Hotels.Update(hotel);
                return Save();
            }

            public bool DeleteHotel(Hotel hotel)
            {
                _context.Hotels.Remove(hotel);
                return Save();
            }

            public bool Save()
            {
                return _context.SaveChanges() > 0;
            }
        }
    }
