using System.Text.Json.Serialization;

namespace Models.DTO
{
    public class EventDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int IdEvent { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateEvent { get; set; }
        public int IdCity { get; set; }
        public string Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int Capacity { get; set; }
        public int IdOwner { get; set; }
    }
}
