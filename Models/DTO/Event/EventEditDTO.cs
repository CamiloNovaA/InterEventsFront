using System.Text.Json.Serialization;

namespace Models.DTO.Event
{
    public class EventEditDTO
    {
        public int IdEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public int IdCity { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int Capacity { get; set; }
        public int IdOwner { get; set; }
    }
}
