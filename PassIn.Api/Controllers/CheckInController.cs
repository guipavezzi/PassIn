using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Checkin.DoCheckIn;
using PassIn.Communication.Responses;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Interfaces.Attendees;

[Route("api/[controller]")]
[ApiController]

public class CheckInController : ControllerBase
{
    private readonly ICheckinRepository _checkinRepository;
    private readonly IAttendeesRepository _attendeesRepository;
    public CheckInController(ICheckinRepository checkinRepository, IAttendeesRepository attendeesRepository)
    {
        _checkinRepository = checkinRepository;
        _attendeesRepository = attendeesRepository;
    }
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckIn([FromRoute] Guid attendeeId)
    {
        var useCase = new DoAttendeeCheckInUseCase(_checkinRepository, _attendeesRepository);

        var response = await useCase.Execute(attendeeId);

        return Created(string.Empty, response);
    }
}