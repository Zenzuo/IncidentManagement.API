using IncidentManagement.API.Models; 

namespace IncidentManagement.API.Dtos
{
    public class UpdateIncidentDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public IncidentStatus Status { get; set; }
    }
}
