using Models.DTO.Attendant;
using Models.Entities.Response;

namespace Models.Interfaces
{
    public interface IAttendantCommands
    {
        Task<bool> ValidateLimitEventsForAttendant(int IdAttendant);
        Task<bool> ValidateOwnerEvent(AttendantRegisterEvent attendantRegister);
        Task<bool> ValidateQuotasEvent(int IdEvent);
        Task<EventResponse> SubscribeForEvent(AttendantRegisterEvent attendantRegister);
        Task<EventResponse> UnsubscribeForEvent(AttendantRegisterEvent attendantRegister);
        Task<List<int>> GetSuscriptions(int idUser);
    }
}
