using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Checkin.DoCheckIn;

public class DoAttendeeCheckInUseCase
{
    private readonly PassInDbContext _context;
    public DoAttendeeCheckInUseCase(PassInDbContext context)
    {
        _context = context;
    }
    public ResponseRegisteredJson Execute(Guid attendeeId)
    {
        Validate(attendeeId);

        var entity = new CheckIn
        {
            Attendee_id = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        _context.CheckIns.Add(entity);
        _context.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }

    private void Validate(Guid attendeeId)
    {
        var existAttendee = _context.Attendees.Any(attendee => attendee.Id == attendeeId);
        if (existAttendee == false)
        {
            throw new NotFoundException("The Attendee with this Id was not found.");
        }

        var existCheckIn = _context.CheckIns.Any(ch => ch.Attendee_id == attendeeId);

        if (existCheckIn)
        {
            throw new ConflictException("Attendee can not do checking twice in the same event.");
        }
    }
}