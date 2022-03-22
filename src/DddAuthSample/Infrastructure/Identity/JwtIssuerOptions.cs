using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity;

public class JwtIssuerOptions
{
	public const string JwtIssuer = "JwtIssuer";

	public string Issuer { get; set; }
	public string Audience { get; set; }
	public int ValidForInMinutes { get; set; }
	public string Secret { get; set; }
}
