using System.Net.Mail;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using PassIn.Infrastructure.Interfaces.Attendees;
using PassIn.Infrastructure.Interfaces.Events;

namespace PassIn.Application.UseCases.Attendees.RegisterAttendee;

public class RegisterAttendeeOnEventUseCase
{
    private readonly IAttendeesRepository _attendeeRepository;
    private readonly IEventRepository _eventRepository;
    public RegisterAttendeeOnEventUseCase(IAttendeesRepository attendeeRepository, IEventRepository eventRepository)
    {
        _attendeeRepository = attendeeRepository;
        _eventRepository = eventRepository;
    }
    public async Task<ResponseRegisteredJson> Execute(Guid eventId, RequestRegisterEventJson request)
    {
        Validate(eventId, request);

        var entity = new Attendee
        {
            Name = request.Name,
            Email = request.Email,
            Event_Id = eventId,
            Created_At = DateTime.UtcNow,
        };

        await _attendeeRepository.Add(entity);

        return new ResponseRegisteredJson
        {
            Id = entity.Id
        };
    }

    private void Validate(Guid eventId, RequestRegisterEventJson request)
    {
        var eventEntity = _eventRepository.GetEventById(eventId);

        if (eventEntity is null)
        {
            throw new NotFoundException("An event with this id dont exists.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidationException("The Name is invalid.");
        }

        var emailIsValid = EmailIsValid(request.Email);
        if (emailIsValid == false)
        {
            throw new ErrorOnValidationException("The e-mail is invalid.");
        }

        if (_attendeeRepository.ExistAttendeeInEvent(request.Email, eventEntity.Result.Id).Result)
        {
            throw new ErrorOnValidationException("You can not register twice on the same event.");
        }

        if (_attendeeRepository.AttendeesForEvent(eventId).Result > eventEntity.Result.Maximum_Attendees)
        {
            throw new ErrorOnValidationException("There is no room for this event.");
        }
    }

    private bool EmailIsValid(string email)
    {
        try
        {
            new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}