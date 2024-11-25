using Models.DTO;
using Models.DTO.Event;
using Models.Entities.Response;
namespace Models.Interfaces
{
    public interface IEventServices
    {
        Task<List<EventResult>> GetEvents();
        Task<List<EventByIdResult>> GetEventsById(int idUser);
        Task<EventResponse> CreateEvent(EventDTO newEvent);
        Task<EventResponse> EditEvent(EventDTO editEvent);
        Task<EventResponse> ChangeStateEvent(EventStateDTO eventState);
        Task<EventResponse> DeleteEvent(EventDeleteDTO eventDelete);
    }
}