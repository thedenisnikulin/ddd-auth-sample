using Chattitude.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Chattitude.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HomeController : ControllerBase
	{
		public HomeController() {}

		[HttpGet]
		public IActionResult Home()
		{
			return Ok("Home");
		}
		[Authorize]
		[HttpGet("authorized")]
		public IActionResult AuthorizedEndpoint()
		{
			return Ok("Secret Info");
		}
	}
}