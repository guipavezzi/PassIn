using Microsoft.EntityFrameworkCore;
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

        public async Task<int> AttendeesForEvent(Guid eventId)
        {
            return await _context.Attendees.CountAsync(attendee => attendee.Event_Id == eventId);
        }

        public async Task<bool> ExistAttendeeInEvent(string email, Guid idEvent)
        {
            return await _context.Attendees.AnyAsync(
                attendee => attendee.Email.Equals(email) && attendee.Event_Id == idEvent
            );
        }
    }
}
