using ComfortSpace.Models;

namespace ComfortSpace.Interfaces
{
    public interface ISensorRepository
    {
        bool CreateSensor(Sensor sensor);

        ICollection<Sensor> GetSensors();
        ICollection<Sensor> GetSensorsByRoom(int roomId);
        Sensor GetSensor(int id);

        bool SensorExists(int id);

        bool UpdateSensor(Sensor sensor);
        bool DeleteSensor(Sensor sensor);

        bool Save();
    }
}
