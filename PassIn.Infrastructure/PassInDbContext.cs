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

        public PassInDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PassInDbContext>();

        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PassInDb;User Id=sa;Password=MinhaSenha123!");

        return new PassInDbContext(optionsBuilder.Options);
    }

        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
    }
}