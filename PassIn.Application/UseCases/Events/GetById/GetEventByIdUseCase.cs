﻿using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure.Interfaces.Events;

namespace PassIn.Application.UseCases.Events.GetById;

public class GetEventByIdUseCase
{
    private readonly IEventRepository _eventRepository;
    public GetEventByIdUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    public ResponseEventJson Execute(Guid id)
    {
        var entity = _eventRepository.GetEventById(id).Result;

        if (entity is null)
        {
            throw new NotFoundException("An event with this id dont exists.");
        }

        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = entity.Attendees.Count(),
        };
    }
}