using IncidentManagement.API.Data;
using IncidentManagement.API.Dtos;
using IncidentManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IncidentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<IncidentsController> _logger;

        public IncidentsController(AppDbContext context, ILogger<IncidentsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        //private static readonly List<Incident> _incidents = new()
        //{
        //    new Incident
        //    {
        //        Id = 1,
        //        Title = "No funciona el correo",
        //        Description = "El correo corporativo no envía mensajes.",
        //        UserId = 101,
        //        CategoryId = 1,
        //        Status = IncidentStatus.Open
        //    },
        //    new Incident
        //    {
        //        Id = 2,
        //        Title = "Error al ingresar al sistema",
        //        Description = "El sistema lanza error 500 al iniciar sesión.",
        //        UserId = 102,
        //        CategoryId = 2,
        //        Status = IncidentStatus.InProgress
        //    }
        //};

        // GET api/incidents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetAll()
        {
            _logger.LogInformation("Obteniendo todos los incidentes");

            var incidents = await _context.Incidents.ToListAsync();

            _logger.LogInformation("Se retornaron {Count} incidentes", incidents.Count);

            return Ok(incidents);
        }


        // GET api/incidents/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Incident>> GetById(int id)
        {
            _logger.LogInformation("Buscando incidente con id {Id}", id);

            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                _logger.LogWarning("Incidente con id {Id} no encontrado", id);
                return NotFound();
            }

            return Ok(incident);
        }


        [HttpPost]
        public async Task<ActionResult<Incident>> CreateIncident([FromBody] CreateIncidentDto dto)
        {
            _logger.LogInformation("Creando incidente para usuario {UserId} en categoría {CategoryId}",
                dto.UserId, dto.CategoryId);

            var newIncident = new Incident
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                Status = IncidentStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            _context.Incidents.Add(newIncident);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Incidente creado con id {Id}", newIncident.Id);

            return CreatedAtAction(nameof(GetById), new { id = newIncident.Id }, newIncident);
        }


        // PUT api/incidents/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Incident>> UpdateIncident(int id, [FromBody] UpdateIncidentDto dto)
        {
            _logger.LogInformation("Actualizando incidente {Id}", id);

            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                _logger.LogWarning("No se pudo actualizar. Incidente {Id} no encontrado", id);
                return NotFound();
            }

            incident.Title = dto.Title;
            incident.Description = dto.Description;
            incident.UserId = dto.UserId;
            incident.CategoryId = dto.CategoryId;
            incident.Status = dto.Status;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Incidente {Id} actualizado correctamente", id);

            return Ok(incident);
        }


        // DELETE api/incidents/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteIncident(int id)
        {
            _logger.LogInformation("Eliminando incidente {Id}", id);

            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                _logger.LogWarning("No se pudo eliminar. Incidente {Id} no encontrado", id);
                return NotFound();
            }

            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Incidente {Id} eliminado correctamente", id);

            return NoContent();
        }


        //COMMENTS

        // POST api/Incidents/{id}/comments
        [HttpPost("{id:int}/comments")]
        public async Task<ActionResult<Comment>> AddComment(int id, [FromBody] CreateCommentDto dto)
        {
            _logger.LogInformation("Agregando comentario al incidente {Id}", id);

            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                _logger.LogWarning("No se pudo agregar comentario. Incidente {Id} no encontrado", id);
                return NotFound($"Incident {id} not found");
            }

            var comment = new Comment
            {
                IncidentId = id,
                Author = dto.Author,
                Text = dto.Text,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Comentario agregado al incidente {Id} con CommentId {CommentId}", id, comment.Id);

            return CreatedAtAction(nameof(GetCommentsForIncident), new { id = id }, comment);
        }


        // GET api/Incidents/{id}/comments
        [HttpGet("{id:int}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForIncident(int id)
        {
            _logger.LogInformation("Obteniendo comentarios del incidente {Id}", id);

            var incidentExists = await _context.Incidents.AnyAsync(i => i.Id == id);
            if (!incidentExists)
            {
                _logger.LogWarning("No se pudieron obtener comentarios. Incidente {Id} no encontrado", id);
                return NotFound($"Incident {id} not found");
            }

            var comments = await _context.Comments
                .Where(c => c.IncidentId == id)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            _logger.LogInformation("Se encontraron {Count} comentarios para el incidente {Id}", comments.Count, id);

            return Ok(comments);
        }



    }
}
