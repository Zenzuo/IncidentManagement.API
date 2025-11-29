using IncidentManagement.Api.Dtos;
using IncidentManagement.API.Dtos;
using IncidentManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IncidentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {
        private static readonly List<Incident> _incidents = new()
        {
            new Incident
            {
                Id = 1,
                Title = "No funciona el correo",
                Description = "El correo corporativo no envía mensajes.",
                UserId = 101,
                CategoryId = 1,
                Status = IncidentStatus.Open
            },
            new Incident
            {
                Id = 2,
                Title = "Error al ingresar al sistema",
                Description = "El sistema lanza error 500 al iniciar sesión.",
                UserId = 102,
                CategoryId = 2,
                Status = IncidentStatus.InProgress
            }
        };

        // GET api/incidents
        [HttpGet]
        public ActionResult<IEnumerable<Incident>> GetAll()
        {
            return Ok(_incidents);
        }
        // GET api/incidents/5
        [HttpGet("{id:int}")]
        public ActionResult<Incident> GetById(int id)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);

            if (incident is null)
                return NotFound();

            return Ok(incident);
        }

        [HttpPost]
        public ActionResult<Incident> CreateIncident([FromBody] CreateIncidentDto dto)
        {
            var newIncident = new Incident
            {
                Id = _incidents.Max(i => i.Id) + 1, 
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                Status = IncidentStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            _incidents.Add(newIncident);

            //return CreatedAtAction(nameof(GetAll), new { id = newIncident.Id }, newIncident);
            return CreatedAtAction(nameof(GetById), new { id = newIncident.Id }, newIncident);

        }
        // PUT api/incidents/5
        [HttpPut("{id:int}")]
        public ActionResult<Incident> UpdateIncident(int id, [FromBody] UpdateIncidentDto dto)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);

            if (incident is null)
                return NotFound();

            incident.Title = dto.Title;
            incident.Description = dto.Description;
            incident.UserId = dto.UserId;
            incident.CategoryId = dto.CategoryId;
            incident.Status = dto.Status;

            return Ok(incident);
        }
        // DELETE api/incidents/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteIncident(int id)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);

            if (incident is null)
                return NotFound();

            _incidents.Remove(incident);

            return NoContent();
        }


    }
}
