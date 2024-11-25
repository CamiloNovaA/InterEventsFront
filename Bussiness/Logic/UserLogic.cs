using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Models.DTO.User;
using Models.Entities.Response;
using Models.Enums;
using Models.Interfaces;
using Models.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bussiness.Logic
{
    public class UserLogic : IUserServices
    {
        private IConfiguration _configuration;
        private IUserCommands _userCommands;

        public UserLogic(IConfiguration configuration, IUserCommands userCommands)
        {
            _configuration = configuration;
            _userCommands = userCommands;
        }

        public async Task<ResultRegisterUser> RegistryUser(UserDTO user)
        {
            ResultRegisterUser result = new();
            UserValidator validationRules = new();

            ValidationResult validationResult = validationRules.Validate(user);

            if (validationResult.IsValid)
            {
                bool emailExists = await _userCommands.ValidateEmail(user.Email);

                if (emailExists)
                {
                    result.Message = SystemMessage.EmailExist;
                    result.Result = false;
                }
                else
                {
                    result = await _userCommands.RegistryUser(user);

                    if (result.Result)
                    {
                        result.Token = await CreateToken(result.IdUser);
                    }
                }
            }
            else
            {
                result.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
            }

            return result;
        }

        public async Task<LoginDetails> UserLogin(UserCredentialDTO credential)
        {
            LoginDetails loginDetails = new();
            loginDetails = await _userCommands.Login(credential);

            if (!loginDetails.IsSuccess)
                return loginDetails;

            loginDetails.Token = await CreateToken(loginDetails.IdUser);

            return loginDetails;
        }

        private async Task<string> CreateToken(int idUser)
        {
            JwtSecurityTokenHandler jwtSecurityToken = new();

            var tokenClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, idUser.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", idUser.ToString())
            };

            string key = _configuration["Jwt:Key"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credencial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(tokenClaims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = credencial
            };

            var createToken = jwtSecurityToken.CreateToken(tokenDecriptor);

            return jwtSecurityToken.WriteToken(createToken);
        }
    }
}
