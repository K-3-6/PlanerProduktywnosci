using Microsoft.EntityFrameworkCore;
using PlanerAPI.Models;

namespace PlanerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Ta właściwość reprezentuje naszą tabelę w bazie danych
        public DbSet<TaskItem> Tasks { get; set; }
    }
}