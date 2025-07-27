using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAllByEventid;
using PassIn.Application.UseCases.Attendees.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Infrastructure.Interfaces.Attendees;
using PassIn.Infrastructure.Interfaces.Events;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    private readonly IEventRepository _eventRepository;
    private readonly IAttendeesRepository _attendeesRepository;
    public AttendeesController(IEventRepository eventRepository, IAttendeesRepository attendeesRepository)
    {
        _eventRepository = eventRepository;
        _attendeesRepository = attendeesRepository;
    }
    [HttpPost]
    [Route("{eventId}/register")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
    {
        var useCase = new RegisterAttendeeOnEventUseCase(_attendeesRepository, _eventRepository);

        var response = await useCase.Execute(eventId, request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAllAttendeesjson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetAll([FromRoute] Guid eventId)
    {
        var useCase = new GetAllAttendeesByEventIdUseCase(_eventRepository);

        var response = useCase.Execute(eventId);

        return Ok(response);
    }
}