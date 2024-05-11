using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Checkin.DoCheckIn;
using PassIn.Communication.Responses;
using PassIn.Infrastructure;

[Route("api/[controller]")]
[ApiController]

public class CheckInController : ControllerBase
{
    private readonly PassInDbContext _context;
    public CheckInController(PassInDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult CheckIn([FromRoute] Guid attendeeId)
    {
        var useCase = new DoAttendeeCheckInUseCase(_context);

        var response = useCase.Execute(attendeeId);

        return Created(string.Empty, response);
    }
}