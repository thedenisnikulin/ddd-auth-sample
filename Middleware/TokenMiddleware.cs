using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Chattitude.Api.Dto;
using Microsoft.Extensions.Logging;
using Chattitude.Api.Services;

namespace Chattitude.Api.Middleware
{
	public class TokenMiddleware
	{
		private ILogger<TokenMiddleware> _logger { get; }
		private readonly RequestDelegate _next;

		public TokenMiddleware(ILogger<TokenMiddleware> logger, RequestDelegate next)
		{
			_logger = logger;
			_next = next;
		}

		public async Task Invoke(HttpContext httpCtx, TokenService tokenService)
		{
			string authToken = httpCtx.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			if (authToken != "")
			{
				_attachUserToCtx(httpCtx, tokenService, authToken);
			}
			await _next(httpCtx);
		}
		public void _attachUserToCtx(HttpContext httpCtx, TokenService tokenService, string token)
		{
			SecurityToken validatedToken = tokenService.Validate(token);
			JwtSecurityToken jwtToken;
			if (validatedToken != null)
			{
				jwtToken = (JwtSecurityToken) validatedToken;
				httpCtx.Items["user"] = (UserDto) JsonSerializer.Deserialize<UserDto>(
					jwtToken.Claims.FirstOrDefault(e => e.Type == "user").Value);
			}
		}
	}
}
