using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Attendant;
using Models.Entities.Response;
using Models.Interfaces;

namespace InterEventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendantController : ControllerBase
    {
        private readonly IAttendantServices _attendantServices;

        public AttendantController(IAttendantServices attendantServices)
        {
            _attendantServices = attendantServices;
        }

        [HttpGet]
        [Authorize]
        [Route("GetSuscriptions/{idUser}")]
        public async Task<IActionResult> GetSuscriptions(int idUser)
        {
            List<int> register = await _attendantServices.GetSuscriptions(idUser);
            return Ok(register);
        }

        [HttpPut]
        [Authorize]
        [Route("SubscribeForEvent")]
        public async Task<IActionResult> SubscribeForEvent([FromBody] AttendantRegisterEvent attendantRegister)
        {
            EventResponse register = await _attendantServices.SubscribeForEvent(attendantRegister);
            return Ok(register);
        }

        [HttpDelete]
        [Authorize]
        [Route("UnsubscribeForEvent")]
        public async Task<IActionResult> UnsubscribeForEvent([FromBody] AttendantRegisterEvent attendantRegister)
        {
            EventResponse register = await _attendantServices.UnsubscribeForEvent(attendantRegister);
            return Ok(register);
        }
    }
}
