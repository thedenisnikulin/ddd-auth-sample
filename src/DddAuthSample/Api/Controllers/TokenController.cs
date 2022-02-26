using Microsoft.AspNetCore.Mvc;
using Identity.Application.Commands;
using MediatR;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
	private readonly ILogger<TokenController> _logger;
	private readonly IMediator _mediator;

	public TokenController(ILogger<TokenController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	[HttpPost("refresh")]
	public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
	{
		var result = await _mediator.Send(command);

		return (result.IsFailed) ? Forbid() : Ok();
	}
}
