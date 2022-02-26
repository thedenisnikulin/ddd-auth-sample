using Identity.Domain.Entities;

namespace Identity.Application.Contracts;

// probably should be abstracted from jwt token impl
public interface ITokenFactory
{
	// Prefer strong return types instead of plain strings
	string GenerateAccessToken(User user);
	string GenerateRefreshToken();
}
