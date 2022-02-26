using Identity.Domain.Entities;
using SeedWork;
using SharedKernel;

namespace Identity.Domain.Events;

public class RefreshSessionCreated : IDomainEvent
{
	public string RefreshToken { get; }
	public UserId UserId { get; }

	public RefreshSessionCreated(string refreshToken, UserId userId)
	{
		RefreshToken = refreshToken;
		UserId = userId;
	}
}
