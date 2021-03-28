using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chattitude.Api.Services;
using Chattitude.Api.Dto;

namespace Chattitude.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private AuthService _authService { get; }
        private TokenService _tokenService { get; }

        public AuthController(AuthService authService, TokenService tokenService)
		{
			_authService = authService;
			_tokenService = tokenService;
		}

		[HttpPost("login")]
		public ActionResult<UserLoginResponseDto> Login([FromBody] UserLoginDto user)
		{
			var userLoginResponseDto = _authService.Login(user);
			if (userLoginResponseDto != null)
			{
				return Ok(userLoginResponseDto);
			}
			else
			{
				return NotFound();
			}
		}
		[HttpPost("register")]
		public async Task<ActionResult<UserRegisterResponseDto>> Register([FromBody] UserRegisterDto user)
		{
			var userRegisterResponseDto = await _authService.Register(user);
			if (userRegisterResponseDto != null)
			{
				return Ok(userRegisterResponseDto);
			}
			else
			{
				return Problem("Credentials are incorrect.");
			}
		}
	}
}
