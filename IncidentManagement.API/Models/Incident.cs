namespace IncidentManagement.API.Models
{
    public enum IncidentStatus
    {
        Open,
        InProgress,
        Closed
    }

    public class Incident
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public IncidentStatus Status { get; set; } = IncidentStatus.Open;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
