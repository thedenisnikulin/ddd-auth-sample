using Identity.Domain.Events;
using SeedWork;
using SharedKernel;

namespace Identity.Domain.Entities;

public class RefreshSession : Entity
{
	public UserId UserId { get; }
	public string RefreshToken { get; private set; }
	public string Ip { get; }
	public DateTime ExpiresAt { get; }
	public DateTime CreatedAt { get; }

	private RefreshSession() {}

	private RefreshSession(
		UserId userId,
		string refreshToken,
		string ip,
		DateTime expiresIn)
	{
		UserId = userId;
		RefreshToken = refreshToken;
		Ip = ip;
		ExpiresAt = expiresIn;
		CreatedAt = DateTime.Now;
	}

	public static RefreshSession Create(
		UserId userId,
		string refreshToken,
		string ip,
		DateTime expiresIn)
	{
		return new RefreshSession(userId, refreshToken, ip, expiresIn);
	}


	public void UpdateRefreshToken(string newRefreshToken)
	{
		RefreshToken = newRefreshToken;
	}
}
