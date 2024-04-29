﻿using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventid;

public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _dbContext;
    public GetAllAttendeesByEventIdUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseAllAttendeesjson Execute(Guid eventId)
    {
        var entity = _dbContext.Events.Include(ev => ev.Attendees).ThenInclude(attendee => attendee.Checkin).FirstOrDefault(ev => ev.Id == eventId);

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