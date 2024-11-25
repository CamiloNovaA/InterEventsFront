using Models.DTO;
using Models.DTO.Event;
using Models.Entities.Response;

namespace Models.Interfaces
{
    public interface IEventCommands
    {
        Task<List<EventResult>> GetEvents();
        Task<List<EventByIdResult>> GetEventsById(int idUsuario);
        Task<EventResponse> CreateEvent(EventDTO newEvent);
        Task<EventResponse> EditEvent(EventDTO editEvent);
        Task<EventResponse> ChangeStateEvent(EventStateDTO editEvent);
        Task<EventResponse> DeleteEvent(EventDeleteDTO deleteEvent);
        Task<bool> EventHasAttendants(int deleteEvent);
    }
}
