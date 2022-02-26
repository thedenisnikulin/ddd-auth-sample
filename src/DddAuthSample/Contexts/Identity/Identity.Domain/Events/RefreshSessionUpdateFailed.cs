using Identity.Domain.Entities;
using SeedWork;

namespace Identity.Domain.Events;

public class RefreshSessionUpdateFailed : IDomainEvent
{
	public string Ip { get; }
	public string RefreshToken { get; }

	public RefreshSessionUpdateFailed(string ip, string refreshToken)
	{
		Ip = ip;
		RefreshToken = refreshToken;
	}
}
