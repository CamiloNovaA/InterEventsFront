using Models.DTO;
using Models.DTO.User;
using Models.Entities.Response;

namespace Models.Interfaces
{
    public interface IUserCommands
    {
        Task<ResultRegisterUser> RegistryUser(UserDTO user);
        Task<bool> ValidateEmail(string email);
        Task<LoginDetails> Login(UserCredentialDTO credentials);
    }
}