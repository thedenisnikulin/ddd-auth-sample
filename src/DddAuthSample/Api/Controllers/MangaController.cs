using Microsoft.AspNetCore.Mvc;
using Manga.Application.Queries;
using Manga.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MangaController : ControllerBase
{
	private readonly ILogger<MangaController> _logger;
	private readonly IMediator _mediator;

	public MangaController(ILogger<MangaController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	[HttpGet("all/by-reader-id")]
	public ActionResult GetAllByAuthorId([FromBody] GetAllMangaByAuthorIdQuery query)
	{
		var result = _mediator.Send(query);
		return Ok(result);
	}

	[HttpGet("all/by-author-id")]
	public ActionResult GetAllByReaderId([FromBody] GetAllMangaByReaderIdQuery query)
	{
		var result = _mediator.Send(query);
		return Ok(result);
	}

	[HttpGet("all")]
	public ActionResult GetAllPaged([FromBody] GetAllMangaPagedQuery query)
	{
		var result = _mediator.Send(query);
		return Ok(result);
	}

	[HttpGet("one")]
	public ActionResult Get([FromBody] GetMangaQuery query)
	{
		var result = _mediator.Send(query);
		return Ok(result);
	}

	[Authorize]
	[HttpPost("author/new")]
	public ActionResult CreateAuthor([FromBody] CreateAuthorCommand command)
	{
		var result = _mediator.Send(command);
		return Ok(result);
	}

	[Authorize]
	[HttpPost("reader/new")]
	public ActionResult CreateReader([FromBody] CreateReaderCommand command)
	{
		var result = _mediator.Send(command);
		return Ok(result);
	}

	// [Authorize]
	[HttpPost("new")]
	public ActionResult PostManga([FromBody] PostMangaCommand command)
	{
		_logger.LogInformation(0, "new");
		_mediator.Send(command);
		return Ok();
	}

	[HttpPost("test")]
	public ActionResult Test([FromBody] string test)
	{
		return Ok(test);
	}

	[Authorize]
	[HttpPost("update")]
	public ActionResult ChangeManga([FromBody] ChangeMangaCommand command)
	{
		_mediator.Send(command);
		return Ok();
	}

	[Authorize]
	[HttpPost("bookmark")]
	public ActionResult BookmarkManga([FromBody] BookmarkMangaCommand command)
	{
		_mediator.Send(command);
		return Ok();
	}
}
