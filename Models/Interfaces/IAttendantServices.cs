using Microsoft.AspNetCore.Mvc;
using Models.DTO.Attendant;
using Models.Entities.Response;

namespace Models.Interfaces
{
    public interface IAttendantServices
    {
        Task<EventResponse> SubscribeForEvent(AttendantRegisterEvent attendantRegister);
        Task<EventResponse> UnsubscribeForEvent(AttendantRegisterEvent attendantRegister);
        Task<List<int>> GetSuscriptions(int idUser);
    }
}
