﻿using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Infrastructure.Interfaces.Events;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EventsController : ControllerBase
{
    private readonly IEventRepository _eventRepository;
    public EventsController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        var useCase = new RegisterEventUseCase(_eventRepository);

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var useCase = new GetEventByIdUseCase(_eventRepository);

        var response = useCase.Execute(id);

        return Ok(response);
    }
}