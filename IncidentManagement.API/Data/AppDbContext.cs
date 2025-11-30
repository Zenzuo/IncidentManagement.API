using IncidentManagement.API.Models; // cambia al namespace de tus Models
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.API.Data  // ajusta al namespace de tu proyecto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
