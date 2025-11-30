namespace IncidentManagement.API.Models   // mismo namespace que Incident
{
    public class Comment
    {
        public int Id { get; set; }

        public int IncidentId { get; set; }   // FK al incidente
        public Incident Incident { get; set; } = null!;

        public string Author { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
