using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Interfaces.Events;

namespace PassIn.Infrastructure.Repositories.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly PassInDbContext _context;
        public EventRepository(PassInDbContext context)
        {
            _context = context;
        }
        public async Task<Event> Add(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Event> GetAllAttendees(Guid eventId)
        {
            return await _context.Events.Include(ev => ev.Attendees).ThenInclude(attendee => attendee.Checkin).FirstOrDefaultAsync(ev => ev.Id == eventId);
        }

        public async Task<Event> GetEventById(Guid id)
        {
             return await _context.Events.Include(ev => ev.Attendees).FirstOrDefaultAsync(ev => ev.Id == id);
        }
    }
}
