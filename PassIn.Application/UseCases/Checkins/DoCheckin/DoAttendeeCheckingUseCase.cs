using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Interfaces.Attendees;

namespace PassIn.Application.UseCases.Checkin.DoCheckIn;

public class DoAttendeeCheckInUseCase
{
    private readonly ICheckinRepository _checkinRepository;
    private readonly IAttendeesRepository _attendeesRepository;
    public DoAttendeeCheckInUseCase(ICheckinRepository checkinRepository, IAttendeesRepository attendeesRepository)
    {
        _checkinRepository = checkinRepository;
        _attendeesRepository = attendeesRepository;
    }
    public async Task<ResponseRegisteredJson> Execute(Guid attendeeId)
    {
        await Validate(attendeeId);

        var entity = new CheckIn
        {
            AttendeeId = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        await _checkinRepository.Add(entity);

        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }

    private async Task Validate(Guid attendeeId)
    {
        var existAttendee = await _attendeesRepository.ExistAttendee(attendeeId);
        if (existAttendee == false)
        {
            throw new NotFoundException("The Attendee with this Id was not found.");
        }

        var existCheckIn = await _checkinRepository.ExistCheckin(attendeeId);

        if (existCheckIn)
        {
            throw new ConflictException("Attendee can not do checking twice in the same event.");
        }
    }
}