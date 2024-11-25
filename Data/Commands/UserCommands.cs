using Data.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Models.DTO;
using Models.DTO.User;
using Models.Entities.Response;
using Models.Interfaces;
using System.Data;

namespace Data.Commands
{
    public class UserCommands : IUserCommands
    {
        private readonly ConnectionStrings _dbContext;

        public UserCommands(IOptions<ConnectionStrings> dbContext)
        {
            _dbContext = dbContext.Value;
        }

        /// <summary>
        /// Registra un usuario
        /// </summary>
        /// <param name="user">Recibe el objeto del usuario</param>
        /// <returns>Si el usuario fue creado con exito</returns>
        public async Task<ResultRegisterUser> RegistryUser(UserDTO user)
        {
            ResultRegisterUser resultCommand = new();

            try
            {
                using var connection = new SqlConnection(_dbContext.EventsDBContext);
                await connection.OpenAsync();
                SqlCommand command = new("SP_RegistryUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);

                resultCommand.IdUser = Convert.ToInt32(command.ExecuteScalar());
                resultCommand.Result = true;
            }
            catch (Exception ex)
            {
                resultCommand.Message = ex.Message;
            }
            
            return resultCommand;
        }

        /// <summary>
        /// Valida que el email no exista en la BD
        /// </summary>
        /// <param name="email">correo a registrar</param>
        /// <returns>Indica si el correo existe</returns>
        public async Task<bool> ValidateEmail(string email)
        {
            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_ValidateEmail", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        /// <summary>
        /// Controla el acceso al portal
        /// </summary>
        /// <param name="credentials">Objeto de credenciales</param>
        /// <returns>Resultado de la autenticación</returns>
        public async Task<LoginDetails> Login(UserCredentialDTO credentials)
        {
            LoginDetails loginDetails = new();

            using var connection = new SqlConnection(_dbContext.EventsDBContext);
            await connection.OpenAsync();
            SqlCommand command = new("SP_Login", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", credentials.Email);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read()) // Si hay un registro
                {
                    loginDetails = new LoginDetails
                    {
                        IsSuccess = BCrypt.Net.BCrypt.Verify(credentials.Password, reader["USR_Password"].ToString()),
                        IdUser = (int)reader["USR_IdUser"],
                        FullName = reader["USR_FullName"].ToString()
                    };
                }
            }

            return loginDetails;
        }
    }
}
