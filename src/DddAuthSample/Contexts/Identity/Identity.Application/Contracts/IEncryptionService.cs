namespace Identity.Application.Contracts;

public interface IEncryptionService
{
	string EncryptPassword(string plainText);
	bool VerifyPassword(string plainText, string hash);
}
