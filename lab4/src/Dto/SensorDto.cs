namespace ComfortSpace.Dto
{
    public class SensorDto
    {
        public int SensorId { get; set; }  

        public int RoomId { get; set; }

        public string Name { get; set; }

        public string? Model { get; set; }

        public string Code { get; set; }

        public string Unit { get; set; }

        public string Status { get; set; }
    }
}
