using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure.Interfaces.Events;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventid;

public class GetAllAttendeesByEventIdUseCase
{
    private readonly IEventRepository _eventRepository;
    public GetAllAttendeesByEventIdUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    public ResponseAllAttendeesjson Execute(Guid eventId)
    {
        var entity = _eventRepository.GetAllAttendees(eventId).Result;

        if (entity is null)
        {
            throw new NotFoundException("An event with this id dont exists.");
        }

        return new ResponseAllAttendeesjson
        {
            Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
                CheckedInAt = attendee.Checkin?.Created_at
            }).ToList()
        };
    }
}