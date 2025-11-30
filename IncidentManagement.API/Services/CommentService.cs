using IncidentManagement.API.Data;
using IncidentManagement.API.Dtos;
using IncidentManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.API.Services
{
    public class CommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> AddCommentAsync(int incidentId, CreateCommentDto dto)
        {
            // Validaciones simples de ejemplo
            if (string.IsNullOrWhiteSpace(dto.Author))
                throw new ArgumentException("Author is required");

            if (string.IsNullOrWhiteSpace(dto.Text))
                throw new ArgumentException("Text is required");

            var incidentExists = await _context.Incidents.AnyAsync(i => i.Id == incidentId);
            if (!incidentExists)
                throw new InvalidOperationException($"Incident {incidentId} not found");

            var comment = new Comment
            {
                IncidentId = incidentId,
                Author = dto.Author,
                Text = dto.Text,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}
