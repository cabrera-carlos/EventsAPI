using System;
using EventManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EventManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepository repository;

        public EventsController(IEventsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Event>))]
        public IActionResult Get() => Ok(repository.GetAll());

        [HttpGet]
        [Route("{id}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        public IActionResult GetById(int id)
        {
            if (id < 1)
                return BadRequest();

            var existingEvent = repository.GetById(id);

            if (existingEvent == null) return NotFound();

            return Ok(existingEvent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type =typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] Event newEvent)
        {
            if (newEvent.Id < 1)
                return BadRequest("Invalid Event Id");

            repository.Add(newEvent);

            return CreatedAtAction(nameof(GetById), new {id = newEvent.Id}, newEvent);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (id < 1)
                return BadRequest("Invalid Event Id");

            try
            {
                repository.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

