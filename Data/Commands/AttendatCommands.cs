using Data.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Models.DTO.Attendant;
using Models.Entities.Response;
using Models.Enums;
using Models.Interfaces;
using System.Data;

namespace Data.Commands
{
    public class AttendatCommands : IAttendantCommands
    {
        private readonly ConnectionStrings _dbContext;

        public AttendatCommands(IOptions<ConnectionStrings> dbContext)
        {
            _dbContext = dbContext.Value;
        }

        /// <summary>
        /// Valid si el usuario supero el limite de eventos
        /// </summary>
        /// <param name="IdAttendant">Id del usuario a registrar en el evento</param>
        /// <returns>Indica si el usuario llego a su limite</returns>
        public async Task<bool> ValidateLimitEventsForAttendant(int IdAttendant)
        {
            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_ValidateLimitEventsForAttendant", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdUser", IdAttendant);

            return Convert.ToInt32(command.ExecuteScalar()) >= 3;
        }

        /// <summary>
        /// Valida si el usuario es el dueño del evento
        /// </summary>
        /// <param name="attendantRegister">Id del usuario a registrar en el evento</param>
        /// <returns>Inidca si el usuario es el propietario del evento</returns>
        public async Task<bool> ValidateOwnerEvent(AttendantRegisterEvent attendantRegister)
        {
            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_ValidateOwnerEvent", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdUser", attendantRegister.IdUser);
            command.Parameters.AddWithValue("@IdEvent", attendantRegister.IdEvent);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        /// <summary>
        /// Valida si el evento llego a su limite de asistentes
        /// </summary>
        /// <param name="idEvent">Id del evento a consultar</param>
        /// <returns>Indica si el evento tiene aforo</returns>
        public async Task<bool> ValidateQuotasEvent(int idEvent)
        {
            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_ValidateQuoteEvent", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdEvent", idEvent);

            return Convert.ToInt32(command.ExecuteScalar()).Equals(1);
        }

        public async Task<EventResponse> SubscribeForEvent(AttendantRegisterEvent attendantRegister)
        {
            EventResponse result = new()
            {
                IdEvent = attendantRegister.IdEvent
            };

            try
            {
                using var connection = new SqlConnection(_dbContext.EventsDBContext);
                await connection.OpenAsync();
                SqlCommand command = new("SP_SubscribeForEvent", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdEvent", attendantRegister.IdEvent);
                command.Parameters.AddWithValue("@IdUser", attendantRegister.IdUser);

                result.Result = command.ExecuteNonQuery() > 0;
                result.Message = result.Result ? SystemMessage.SuscribeAttendantForEvent : SystemMessage.CannotSuscribeForEvent;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<EventResponse> UnsubscribeForEvent(AttendantRegisterEvent attendantRegister)
        {
            EventResponse result = new()
            {
                IdEvent = attendantRegister.IdEvent
            };

            try
            {
                using var connection = new SqlConnection(_dbContext.EventsDBContext);
                await connection.OpenAsync();
                SqlCommand command = new("SP_UnsubscribeForEvent", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdEvent", attendantRegister.IdEvent);
                command.Parameters.AddWithValue("@IdUser", attendantRegister.IdUser);

                result.Result = command.ExecuteNonQuery() > 0;
                result.Message = result.Result ? SystemMessage.UnsuscribeAttendantForEvent : SystemMessage.CannotUnsuscribeForEvent;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<List<int>> GetSuscriptions(int idUser)
        {
            List<int> events = new();

            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_GetSuscriptions", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdUser", idUser);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        events.Add((int)reader["EVT_IdEvent"]);
                    }
                }
            }

            return events;
        }
    }
}
