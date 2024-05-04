using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Interfaces.Attendees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Repositories.Attendees
{
    public class AttendeeRepository : IAttendeesRepository
    {
        private readonly PassInDbContext _context;
        public AttendeeRepository(PassInDbContext context)
        {

            _context = context;

        }
        public async Task<Attendee> Add(Attendee entity)
        {
            await _context.Attendees.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
