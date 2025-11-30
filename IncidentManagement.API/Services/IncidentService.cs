using IncidentManagement.API.Data;
using IncidentManagement.API.Dtos;
using IncidentManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.API.Services
{
    public class IncidentService
    {
        private readonly AppDbContext _context;

        public IncidentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Incident> CreateIncidentAsync(CreateIncidentDto dto)
        {
            // Validación sencilla de ejemplo
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title is required");

            var incident = new Incident
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                Status = IncidentStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return incident;
        }

        public async Task<Incident?> UpdateIncidentAsync(int id, UpdateIncidentDto dto)
        {
            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return null; // incidente no existe
            }

            incident.Title = dto.Title;
            incident.Description = dto.Description;
            incident.UserId = dto.UserId;
            incident.CategoryId = dto.CategoryId;
            incident.Status = dto.Status;

            await _context.SaveChangesAsync();

            return incident;
        }
    }
}
