using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.DTO.User;
using Models.Entities.Response;
using Models.Interfaces;

namespace InterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userLogic)
        {
            _userServices = userLogic;
        }

        [HttpPost]
        [Route("RegistryUser")]
        public async Task<IActionResult> RegistryUser([FromBody] UserDTO user)
        {
            ResultRegisterUser resultCommand = new();
            resultCommand = await _userServices.RegistryUser(user);

            return resultCommand.Result ? Ok(resultCommand) : BadRequest(resultCommand);
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] UserCredentialDTO credential)
        {
            LoginDetails loginDetails = new();
            loginDetails = await _userServices.UserLogin(credential);

            return loginDetails.IsSuccess ? Ok(loginDetails) : BadRequest(loginDetails);
        }
    }
}
