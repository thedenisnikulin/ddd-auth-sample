using Identity.Domain.Entities;
using SeedWork;

namespace Identity.Domain.Events;

public class RefreshSessionUpdated : IDomainEvent
{
	public string Ip { get; }
	public string RefreshToken { get; }

	public RefreshSessionUpdated(string ip, string refreshToken)
	{
		Ip = ip;
		RefreshToken = refreshToken;
	}
}
