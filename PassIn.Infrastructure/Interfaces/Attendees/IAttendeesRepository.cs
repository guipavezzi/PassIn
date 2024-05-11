using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure.Interfaces.Attendees
{
    public interface IAttendeesRepository
    {
        Task<Attendee> Add(Attendee entity);
        Task<bool> ExistAttendeeInEvent(string email, Guid idEvent);
        Task<int> AttendeesForEvent(Guid eventId);
    }
}
