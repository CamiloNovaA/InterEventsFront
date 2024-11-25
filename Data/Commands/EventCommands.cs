using Data.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Models.DTO;
using Models.DTO.Event;
using Models.Entities.Response;
using Models.Enums;
using Models.Interfaces;
using System.Data;

namespace Data.Commands
{
    public class EventCommands : IEventCommands
    {
        private readonly ConnectionStrings _dbContext;

        public EventCommands(IOptions<ConnectionStrings> dbContext)
        {
            _dbContext = dbContext.Value;
        }
        
        public async Task<List<EventByIdResult>> GetEventsById(int idUser)
        {
            List<EventByIdResult> events = new();
            EventByIdResult eventDetail = new();

            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_GetEventsById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdUser", idUser);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // Si hay un registro
                {
                    while (reader.Read())
                    {
                        eventDetail = new EventByIdResult
                        {
                            IdEvent = (int)reader["EVT_IdEvent"],
                            IdEventSuscription = (int)reader["Suscrito"],
                            Name = reader["EVT_EventName"].ToString(),
                            Description = reader["EVT_Description"].ToString(),
                            DateEvent = DateTime.Parse(reader["EVT_Date"].ToString() ?? ""),
                            IdCity = reader["EVT_IdCity"].ToString(),
                            NameCity = reader["CTY_Name"].ToString(),
                            Address = reader["EVT_Address"].ToString(),
                            Latitude = reader["EVT_Latitude"].ToString(),
                            Longitude = reader["EVT_Longitude"].ToString(),
                            Capacity = (int)reader["EVT_Capacity"],
                            Owner = (int)reader["EVT_IdOwner"],
                            CreationDate = DateTime.Parse(reader["EVT_CreationDate"].ToString() ?? "")
                        };

                        events.Add(eventDetail);
                    }
                }
            }

            return events;
        }

        public async Task<List<EventResult>> GetEvents()
        {
            List<EventResult> events = new();
            EventResult eventDetail = new();

            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_GetEvents", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // Si hay un registro
                {
                    while(reader.Read())
                    {
                        eventDetail = new EventResult
                        {
                            IdEvent = (int)reader["EVT_IdEvent"],
                            Name = reader["EVT_EventName"].ToString(),
                            Description = reader["EVT_Description"].ToString(),
                            DateEvent = DateTime.Parse(reader["EVT_Date"].ToString() ?? ""),
                            IdCity = reader["EVT_IdCity"].ToString(),
                            NameCity = reader["CTY_Name"].ToString(),
                            Address = reader["EVT_Address"].ToString(),
                            Latitude = reader["EVT_Latitude"].ToString(),
                            Longitude = reader["EVT_Longitude"].ToString(),
                            Capacity = (int)reader["EVT_Capacity"],
                            Owner = (int)reader["EVT_IdOwner"],
                            CreationDate = DateTime.Parse(reader["EVT_CreationDate"].ToString() ?? "")
                        };

                        events.Add(eventDetail);
                    }
                }
            }

            return events;
        }

        public async Task<bool> EventHasAttendants(int IdEvent)
        {
            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_EventHasAttendants", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdEvent", IdEvent);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        public async Task<EventResponse> CreateEvent(EventDTO editEvent)
        {
            EventResponse result = new();

            try
            {
                using var connection = new SqlConnection(_dbContext.EventsDBContext);
                await connection.OpenAsync();
                SqlCommand command = new("SP_CreateEvent", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Name", editEvent.Name);
                command.Parameters.AddWithValue("@Description", editEvent.Description);
                command.Parameters.AddWithValue("@EventDate", editEvent.DateEvent);
                command.Parameters.AddWithValue("@IdCity", editEvent.IdCity);
                command.Parameters.AddWithValue("@Address", editEvent.Address);
                command.Parameters.AddWithValue("@Latitude", editEvent.Latitude);
                command.Parameters.AddWithValue("@Longitude", editEvent.Longitude);
                command.Parameters.AddWithValue("@Capacity", editEvent.Capacity);
                command.Parameters.AddWithValue("@IdOwner", editEvent.IdOwner);

                result.IdEvent = Convert.ToInt32(command.ExecuteScalar());
                result.Result = true;
                result.Message = SystemMessage.EventCreated;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<EventResponse> EditEvent(EventDTO editEvent)
        {
            EventResponse result = new();

            try
            {
                using var connection = new SqlConnection(_dbContext.EventsDBContext);
                await connection.OpenAsync();
                SqlCommand command = new("SP_EditEvent", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdEvent", editEvent.IdEvent);
                command.Parameters.AddWithValue("@IdOwner", editEvent.IdOwner);
                command.Parameters.AddWithValue("@EventDate", editEvent.DateEvent);
                command.Parameters.AddWithValue("@IdCity", editEvent.IdCity);
                command.Parameters.AddWithValue("@Latitude", editEvent.Latitude);
                command.Parameters.AddWithValue("@Longitude", editEvent.Longitude);
                command.Parameters.AddWithValue("@Capacity", editEvent.Capacity);

                result.Result = command.ExecuteNonQuery() > 0;
                result.Message = result.Result ? SystemMessage.EventEdited : SystemMessage.EventCannotEdit;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<EventResponse> ChangeStateEvent(EventStateDTO stateEvent)
        {
            EventResponse result = new();

            try
            {
                using var connection = new SqlConnection(_dbContext.EventsDBContext);
                await connection.OpenAsync();
                SqlCommand command = new("SP_ChangeStateEvent", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdEvent", stateEvent.IdEvent);
                command.Parameters.AddWithValue("@Active", stateEvent.IsActive);
                command.Parameters.AddWithValue("@IdOwner", stateEvent.IdOwner);

                result.Result = command.ExecuteNonQuery() > 0;
                result.Message = result.Result ? 
                    (stateEvent.IsActive ? SystemMessage.EventActive : SystemMessage.EventInactive) :
                    SystemMessage.EventCannotChangeState;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<EventResponse> DeleteEvent(EventDeleteDTO deleteEvent)
        {
            EventResponse result = new();

            try
            {
                using var connection = new SqlConnection(_dbContext.EventsDBContext);
                await connection.OpenAsync();
                SqlCommand command = new("SP_DeleteEvent", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdEvent", deleteEvent.IdEvent);
                command.Parameters.AddWithValue("@IdOwner", deleteEvent.IdUser);

                result.Result = command.ExecuteNonQuery() > 0;
                result.Message = result.Result ? SystemMessage.EventEliminated : SystemMessage.EventCannotEliminate;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}