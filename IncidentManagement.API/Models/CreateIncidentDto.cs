namespace IncidentManagement.API.Dtos  
{
    public class CreateIncidentDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
