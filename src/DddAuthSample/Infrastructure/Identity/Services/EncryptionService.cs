using Identity.Application.Contracts;

namespace Infrastructure.Identity.Services;

public class EncryptionService : IEncryptionService
{
	public string EncryptPassword(string plainText)
	{
		return BCrypt.Net.BCrypt.HashPassword(plainText);
	}

	public bool VerifyPassword(string plainText, string hash)
	{
		return BCrypt.Net.BCrypt.Verify(plainText, hash);
	}
}
