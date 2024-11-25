using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.DTO.Event;
using Models.Entities.Response;
using Models.Interfaces;

namespace InterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventServices _eventServices;

        public EventsController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        [HttpGet]
        [Route("GetEventsById/{idUser}")]
        public async Task<IActionResult> GetEventsById(int idUser)
        {
            List<EventByIdResult> events = await _eventServices.GetEventsById(idUser);

            return Ok(events);
        }

        [HttpGet]
        [Route("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            List<EventResult> events = await _eventServices.GetEvents();

            return Ok(events);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDTO newEvent)
        {
            EventResponse eventCreated = await _eventServices.CreateEvent(newEvent);

            return eventCreated.Result ? Ok(eventCreated) : BadRequest(eventCreated);
        }

        [HttpPut]
        [Authorize]
        [Route("EditEvent")]
        public async Task<IActionResult> EditEvent([FromBody] EventDTO editEvent)
        {
            EventResponse eventEdited = await _eventServices.EditEvent(editEvent);

            return eventEdited.Result ? Ok(eventEdited) : BadRequest(eventEdited);
        }

        [HttpPut]
        [Authorize]
        [Route("ChangeStateEvent")]
        public async Task<IActionResult> ChangeStateEvent([FromBody] EventStateDTO eventState)
        {
            EventResponse eventStateChanged = await _eventServices.ChangeStateEvent(eventState);

            return eventStateChanged.Result ? Ok(eventStateChanged) : BadRequest(eventStateChanged);
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent([FromBody] EventDeleteDTO eventState)
        {
            EventResponse eventStateChanged = await _eventServices.DeleteEvent(eventState);

            return eventStateChanged.Result ? Ok(eventStateChanged) : BadRequest(eventStateChanged);
        }
    }
}