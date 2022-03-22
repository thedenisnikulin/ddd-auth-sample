using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using JWT.Builder;
using JWT.Algorithms;
using Identity.Domain.Entities;
using Identity.Application.Contracts;

namespace Infrastructure.Identity.Services;

public class JwtFactory : ITokenFactory
{
	private readonly JwtIssuerOptions _jwtOptions;

	public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
	{
		_jwtOptions = jwtOptions.Value;
	}

	public string GenerateAccessToken(User user)
	{
		var now = DateTimeOffset.Now;
		return JwtBuilder.Create()
			.WithAlgorithm(new HMACSHA256Algorithm())
			.WithSecret(_jwtOptions.Secret)
			.AddClaim(JwtRegisteredClaimNames.Sub, user.Id.Value)
			.AddClaim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer)
			.AddClaim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience)
			.AddClaim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds())
			.AddClaim(JwtRegisteredClaimNames.Exp, now.AddMinutes(_jwtOptions.ValidForInMinutes).ToUnixTimeSeconds())
			.Encode();
	}

	public string GenerateRefreshToken()
	{
		var bytes = new byte[32];
		using var rand = RandomNumberGenerator.Create();
		rand.GetBytes(bytes);
		return Convert.ToBase64String(bytes);
	}
}
