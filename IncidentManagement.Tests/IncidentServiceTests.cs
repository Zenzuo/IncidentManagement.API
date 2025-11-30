using IncidentManagement.API.Data;
using IncidentManagement.API.Dtos;
using IncidentManagement.API.Models;
using IncidentManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IncidentManagement.Tests
{
    public class IncidentServiceTests
    {
        private AppDbContext CreateInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateIncidentAsync_WithValidData_ShouldCreateIncidentWithOpenStatus()
        {
            // Arrange
            var context = CreateInMemoryDbContext("CreateIncidentDb");
            var service = new IncidentService(context);

            var dto = new CreateIncidentDto
            {
                Title = "Prueba de incidente",
                Description = "Descripción de prueba",
                UserId = 1,
                CategoryId = 1
            };

            // Act
            var incident = await service.CreateIncidentAsync(dto);

            // Assert
            Assert.NotNull(incident);
            Assert.Equal("Prueba de incidente", incident.Title);
            Assert.Equal(IncidentStatus.Open, incident.Status);

            var fromDb = await context.Incidents.FindAsync(incident.Id);
            Assert.NotNull(fromDb);
        }

        [Fact]
        public async Task UpdateIncidentAsync_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            var context = CreateInMemoryDbContext("UpdateIncidentDb");
            var service = new IncidentService(context);

            var dto = new UpdateIncidentDto
            {
                Title = "Nuevo título",
                Description = "Nueva descripción",
                UserId = 1,
                CategoryId = 1,
                Status = IncidentStatus.InProgress
            };

            int nonExistingId = 999; // este id no existe en la BD de prueba

            // Act
            var result = await service.UpdateIncidentAsync(nonExistingId, dto);

            // Assert
            Assert.Null(result);
        }
    }
}
