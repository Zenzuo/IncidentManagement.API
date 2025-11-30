using IncidentManagement.API.Data;
using IncidentManagement.API.Dtos;
using IncidentManagement.API.Models;
using IncidentManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IncidentManagement.Tests
{
    public class CommentServiceTests
    {
        private AppDbContext CreateInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddCommentAsync_WithValidData_ShouldCreateComment()
        {
            // Arrange
            var context = CreateInMemoryDbContext("CommentsDb1");

            var incident = new Incident
            {
                Title = "Incidente para comentarios",
                Description = "Desc",
                UserId = 1,
                CategoryId = 1,
                Status = IncidentStatus.Open,
                CreatedAt = DateTime.UtcNow
            };
            context.Incidents.Add(incident);
            await context.SaveChangesAsync();

            var service = new CommentService(context);

            var dto = new CreateCommentDto
            {
                Author = "Soporte",
                Text = "Estamos revisando el incidente."
            };

            // Act
            var comment = await service.AddCommentAsync(incident.Id, dto);

            // Assert
            Assert.NotNull(comment);
            Assert.Equal(incident.Id, comment.IncidentId);
            Assert.Equal("Soporte", comment.Author);
            Assert.Equal("Estamos revisando el incidente.", comment.Text);

            var fromDb = await context.Comments.FindAsync(comment.Id);
            Assert.NotNull(fromDb);
        }

        [Fact]
        public async Task AddCommentAsync_WithNonExistingIncident_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var context = CreateInMemoryDbContext("CommentsDb2");
            var service = new CommentService(context);

            var dto = new CreateCommentDto
            {
                Author = "Soporte",
                Text = "Comentario para incidente inexistente"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => service.AddCommentAsync(999, dto)); // 999 no existe
        }
    }
}
