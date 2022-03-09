using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity;

public class JwtIssuerOptions
{
	public const string JwtIssuer = "JwtIssuer";

	public string Issuer { get; set; }
	public int ValidForInMinutes { get; set; }
	public string Secret { get; set; }
}
