using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.DTO.Attendant;
using Models.Entities.Response;
using Models.Enums;
using Models.Interfaces;

namespace Bussiness.Logic
{
    public class AttendantLogic : IAttendantServices
    {
        private IConfiguration _configuration;
        private IAttendantCommands _attendantCommand;

        public AttendantLogic(IAttendantCommands attendantCommand, IConfiguration configuration)
        {
            _attendantCommand = attendantCommand;
            _configuration = configuration;
        }

        /// <summary>
        /// Permite a un usuario registrarse en un evento
        /// </summary>
        /// <param name="attendantRegister">Objeto de suscripción</param>
        /// <returns>Resultado de la suscripción</returns>
        public async Task<EventResponse> SubscribeForEvent(AttendantRegisterEvent attendantRegister)
        {
            EventResponse response = new();

            bool hasLimitOfEvents = await _attendantCommand.ValidateLimitEventsForAttendant(attendantRegister.IdUser);

            if (hasLimitOfEvents)
            {
                response.Message = SystemMessage.RegistryWithLimitForEvents;
                return response;
            }

            bool isOwner = await _attendantCommand.ValidateOwnerEvent(attendantRegister);

            if (isOwner)
            {
                response.Message = SystemMessage.RegistryEventOfMyProperty;
                return response;
            }

            bool hasntQuotas = await _attendantCommand.ValidateQuotasEvent(attendantRegister.IdEvent);

            if (hasntQuotas)
            {
                response.Message = SystemMessage.CannotSuscribeForLimit;
            }
            else
            {
                response = await _attendantCommand.SubscribeForEvent(attendantRegister);
            }

            return response;
        }

        /// <summary>
        /// Permite a un usuario cancelar la asistencia a un evento
        /// </summary>
        /// <param name="attendantRegister">Objeto con idEevento y idUsuario</param>
        /// <returns>Resultado de la desuscripción</returns>
        public async Task<EventResponse> UnsubscribeForEvent(AttendantRegisterEvent attendantRegister)
        {
            return await _attendantCommand.UnsubscribeForEvent(attendantRegister);
        }

        /// <summary>
        /// Obtiene los eventos a los que esta suscrito un usuario
        /// </summary>
        /// <param name="idUser">Id usuario a consultar</param>
        /// <returns>Lista de eventos a los que esta suscrito el usuario</returns>
        public async Task<List<int>> GetSuscriptions(int idUser)
        {
            return await _attendantCommand.GetSuscriptions(idUser);
        }
    }
}