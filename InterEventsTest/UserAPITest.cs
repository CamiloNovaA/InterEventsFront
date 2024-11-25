using Bussiness.Users;
using Data.Commands;
using Data.Infrastructure;
using InterEvents.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models.DTO;
using Models.Interfaces;

namespace InterEventsTest.Tests
{
    public class UserAPITest
    {
        private UsersController _usersController;
        private UserCommands _userCommand;
        private UserLogic _userLogic;
        private IUserServices _userService;
        private IConfiguration _configuration;
        private IOptions<ConnectionStrings> _options;

        public UserAPITest()
        {
            _userCommand = new UserCommands(_options);
            _userService = new UserLogic(_configuration, _userCommand);
            _usersController = new UsersController(_userLogic, _userService);
        }

        [Fact]
        public async Task RegistryUser()
        {
            Random random = new Random();

            UserDTO user = new()
            {
                Name = "Usuario",
                Email = $"correo{random.Next()}@correoprueba.com",
                LastName = "Prueba",
                Password = "123456"
            };

            var result = _usersController.RegistryUser(user);

            Assert.IsType<Ok>(result);
        }

        [Fact]
        public async Task ValidateEmail()
        {
            UserDTO user = new()
            {
                Name = "Prueba"
            };

            var result = _usersController.RegistryUser(user);


        }
    }
}
