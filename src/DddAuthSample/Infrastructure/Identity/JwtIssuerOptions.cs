using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity;

public class JwtIssuerOptions
{
	public const string JwtIssuer = "JwtIssuer";

	public string Issuer { get; }
	public int ValidForInMinutes { get; }
	public string Secret { get; }
}
