using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure
{
    public class PassInDbContext : DbContext
    {
        public PassInDbContext(DbContextOptions<PassInDbContext> opt) : base(opt)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
    }
}