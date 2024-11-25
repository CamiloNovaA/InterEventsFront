namespace Models.Entities.Response
{
    public class EventResult
    {
        public int IdEvent { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateEvent { get; set; }
        public string? IdCity { get; set; }
        public string? NameCity { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Address { get; set; }
        public int Capacity { get; set; }
        public int Owner { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
