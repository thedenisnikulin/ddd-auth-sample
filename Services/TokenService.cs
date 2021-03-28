using Chattitude.Api.Dto;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Chattitude.Api.Entities;
using Microsoft.Extensions.Configuration;

namespace Chattitude.Api.Services
{
    public class TokenService
	{
		private ILogger<TokenService> _logger { get; }
		private IConfiguration _config { get; }
		public JwtSecurityTokenHandler TokenHandler { get; } = new JwtSecurityTokenHandler();
		public TokenService(ILogger<TokenService> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}
		public string Generate(User user)
		{
			var secKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(_config["Auth:SecretKey"]));
			var secToken = new JwtSecurityToken(
				signingCredentials: new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256),
				issuer: _config["Auth:Issuer"],
				audience: _config["Auth:Audience"],
				claims: new[]
				{
					new Claim("user", JsonSerializer.Serialize(user))
				},
				expires: DateTime.UtcNow.AddDays(7)
			);
			return TokenHandler.WriteToken(secToken);
		}
		public SecurityToken Validate(string token)
		{
			var secKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(_config["Auth:SecretKey"]));
			var validationParams = new TokenValidationParameters()
			{
				ValidateLifetime = true,
				ValidateAudience = true,
				ValidIssuer = _config["Auth:Issuer"],
				ValidAudience = _config["Auth:Audience"],
				IssuerSigningKey = secKey
			};
			SecurityToken validatedToken;
			try
			{
				TokenHandler.ValidateToken(token, validationParams, out validatedToken);
			}
			catch(SecurityTokenException)
			{
				return null;
			}
			catch(Exception e)
			{
				_logger.LogInformation(
					"damn boy where'd you find dis\n"
					+ e.ToString()
				);
				return null;
			}
			return validatedToken;
		}
	}
}