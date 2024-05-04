using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Repositories.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly PassInDbContext _context;
        public EventRepository()
        {
            _context = new PassInDbContext();
        }
        public async Task<Event> Add(Event entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Event> GetEventById(Guid id)
        {
             return await _context.Events.Include(ev => ev.Attendees).FirstOrDefaultAsync(ev => ev.Id == id);
        }
    }
}
