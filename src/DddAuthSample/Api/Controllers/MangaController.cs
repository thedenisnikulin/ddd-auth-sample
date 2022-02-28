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

	[HttpGet("reader/{readerId}")]
	public async Task<ActionResult> GetAllByAuthorId([FromQuery] GetAllMangaByAuthorIdQuery query)
	{
		var result = await _mediator.Send(query);
		return Ok(result);
	}

	[HttpGet("author/{authorId}")]
	public async Task<ActionResult> GetAllByReaderId([FromQuery] GetAllMangaByReaderIdQuery query)
	{
		var result = await _mediator.Send(query);
		return Ok(result);
	}

	[HttpGet("{mangaPerPage}")]
	public async Task<ActionResult> GetAllPaged([FromQuery] GetAllMangaPagedQuery query)
	{
		var result = await _mediator.Send(query);
		return Ok(result);
	}

	[HttpGet("{mangaId}")]
	public async Task<ActionResult> Get([FromQuery] GetMangaQuery query)
	{
		var result = await _mediator.Send(query);
		return Ok(result);
	}

	[Authorize]
	[HttpPost("author/new")]
	public async Task<ActionResult> CreateAuthor([FromBody] CreateAuthorCommand command)
	{
		var result = await _mediator.Send(command);
		return Ok(result);
	}

	[Authorize]
	[HttpPost("reader/new")]
	public async Task<ActionResult> CreateReader([FromBody] CreateReaderCommand command)
	{
		var result = await _mediator.Send(command);
		return Ok(result);
	}

	[Authorize]
	[HttpPost("new")]
	public async Task<ActionResult> PostManga([FromBody] PostMangaCommand command)
	{
		await _mediator.Send(command);
		return Ok();
	}

	[Authorize]
	[HttpPost("update")]
	public async Task<ActionResult> ChangeManga([FromBody] ChangeMangaCommand command)
	{
		await _mediator.Send(command);
		return Ok();
	}

	[Authorize]
	[HttpPost("reader/bookmark")]
	public async Task<ActionResult> BookmarkManga([FromBody] BookmarkMangaCommand command)
	{
		await _mediator.Send(command);
		return Ok();
	}
}
