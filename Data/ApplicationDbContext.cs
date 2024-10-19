using Microsoft.EntityFrameworkCore;

using NewWebApi.Models;

namespace NewWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
    }
}
