using Microsoft.AspNetCore.Mvc;
using Identity.Application.Commands;
using MediatR;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly ILogger<UserController> _logger;
	private readonly IMediator _mediator;

	public UserController(ILogger<UserController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	[HttpPost("register")]
	public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
	{
		await _mediator.Send(command);
		return Ok();
	}

	[HttpPost("signin")]
	public async Task<ActionResult> SignIn([FromBody] SignInUserCommand query)
	{
		var result = await _mediator.Send(query);
		if (result.IsFailed)
		{
			return NotFound();
		}
		return Ok(result.Value);
	}
}
