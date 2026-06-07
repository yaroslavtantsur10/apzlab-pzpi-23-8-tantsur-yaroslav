
    using ComfortSpace.Data;
    using ComfortSpace.Interfaces;
    using ComfortSpace.Models;

    namespace ComfortSpace.Repository
    {
        public class SensorRepository : ISensorRepository
        {
            private readonly DataContext _context;

            public SensorRepository(DataContext context)
            {
                _context = context;
            }

            public bool CreateSensor(Sensor sensor)
            {
                _context.Sensors.Add(sensor);
                return Save();
            }

            public ICollection<Sensor> GetSensors()
            {
                return _context.Sensors
                    .OrderBy(s => s.SensorId)
                    .ToList();
            }

            public ICollection<Sensor> GetSensorsByRoom(int roomId)
            {
                return _context.Sensors
                    .Where(s => s.RoomId == roomId)
                    .OrderBy(s => s.SensorId)
                    .ToList();
            }

            public Sensor GetSensor(int id)
            {
                return _context.Sensors
                    .FirstOrDefault(s => s.SensorId == id);
            }

            public bool SensorExists(int id)
            {
                return _context.Sensors
                    .Any(s => s.SensorId == id);
            }

            public bool UpdateSensor(Sensor sensor)
            {
                _context.Sensors.Update(sensor);
                return Save();
            }

            public bool DeleteSensor(Sensor sensor)
            {
                _context.Sensors.Remove(sensor);
                return Save();
            }

            public bool Save()
            {
                return _context.SaveChanges() > 0;
            }
        }
    }
