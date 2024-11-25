using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Models.DTO;
using Models.DTO.Event;
using Models.Entities.Response;
using Models.Enums;
using Models.Interfaces;
using Models.Validators;

namespace Bussiness.Logic
{
    public class EventLogic : IEventServices
    {
        private IConfiguration _configuration;
        private IEventCommands _eventCommand;

        public EventLogic(IConfiguration configuration, IEventCommands eventCommands)
        {
            _configuration = configuration;
            _eventCommand = eventCommands;
        }

        /// <summary>
        /// Obtiene todos los eventos activos
        /// </summary>
        /// <returns>Eventos activos</returns>
        public async Task<List<EventResult>> GetEvents()
        {
            return await _eventCommand.GetEvents();
        }

        /// <summary>
        /// Obtiene los eventos indicando si esta inscrito a un evento
        /// </summary>
        /// <returns></returns>
        public async Task<List<EventByIdResult>> GetEventsById(int idUsuario)
        {
            return await _eventCommand.GetEventsById(idUsuario);
        }
        

        /// <summary>
        /// Crea un evento
        /// </summary>
        /// <param name="newEvent">Objeto del nuevo evento</param>
        /// <returns>Objeto con los datos del evento creado</returns>
        public async Task<EventResponse> CreateEvent(EventDTO newEvent)
        {
            EventResponse createdEvent = new();
            EventValidator validationRules = new();
            ValidationResult validationResult = validationRules.Validate(newEvent);

            if (validationResult.IsValid)
            {
                createdEvent = await _eventCommand.CreateEvent(newEvent);
            }
            else
            {
                createdEvent.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
            }

            return createdEvent;
        }

        /// <summary>
        /// Edita un evento
        /// </summary>
        /// <param name="editEvent">Objeto de evento a editar</param>
        /// <returns>Objeto con los datos del evento editado</returns>
        public async Task<EventResponse> EditEvent(EventDTO editEvent)
        {
            EventResponse createdEvent = new();
            EventValidator validationRules = new();
            ValidationResult validationResult = validationRules.Validate(editEvent);

            if (validationResult.IsValid)
            {
                createdEvent = await _eventCommand.EditEvent(editEvent);
            }
            else
            {
                createdEvent.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
            }

            return createdEvent;
        }

        /// <summary>
        /// Activa o inactiva un evento
        /// </summary>
        /// <param name="eventState">Objeto del evento a cambiar estado</param>
        /// <returns>objeto con el resultado de la actualización</returns>
        public async Task<EventResponse> ChangeStateEvent(EventStateDTO eventState)
        {
            return await _eventCommand.ChangeStateEvent(eventState);
        }

        /// <summary>
        /// Elimina un evento si no tiene asistentes
        /// </summary>
        /// <param name="eventDelete">Objeto del evento a eliminar</param>
        /// <returns>Respuesta de la eliminación</returns>
        public async Task<EventResponse> DeleteEvent(EventDeleteDTO eventDelete)
        {
            bool hasAttendants = await _eventCommand.EventHasAttendants(eventDelete.IdEvent);

            if (hasAttendants)
            {
                return new EventResponse
                {
                    IdEvent = eventDelete.IdEvent,
                    Message = SystemMessage.EventHasAttendants,
                    Result = false
                };
            }
            else
            {
                return await _eventCommand.DeleteEvent(eventDelete);
            }
        }
    }
}