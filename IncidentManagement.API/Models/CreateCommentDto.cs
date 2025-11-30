namespace IncidentManagement.API.Dtos
{
    public class CreateCommentDto
    {
        public string Author { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
