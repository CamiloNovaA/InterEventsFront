using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.DTO.User;
using Models.Entities.Response;

namespace Models.Interfaces
{
    public interface IUserServices
    {
        Task<ResultRegisterUser> RegistryUser(UserDTO user);
        Task<LoginDetails> UserLogin(UserCredentialDTO credential);
    }
}
