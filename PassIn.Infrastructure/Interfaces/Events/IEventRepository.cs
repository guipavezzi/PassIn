using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure.Interfaces.Events
{
    public interface IEventRepository
    {
        Task<Event> Add(Event entity);
        Task<Event> GetEventById(Guid id);
        Task<Event> GetAllAttendees(Guid eventId);
    }
}
