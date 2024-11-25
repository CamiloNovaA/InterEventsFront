namespace Models.DTO.Event
{
    public class EventStateDTO
    {
        public int IdEvent { get; set; }
        public int IdOwner { get; set; }
        public bool IsActive { get; set; }
    }
}