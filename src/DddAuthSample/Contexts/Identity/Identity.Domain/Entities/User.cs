using SeedWork;
using SharedKernel;
using Identity.Domain.Events;
using System;

namespace Identity.Domain.Entities;

public class User : Entity, IAggregateRoot
{
	public UserId Id { get; }
	public string Name { get; private set; }
	public string HashedPassword { get; private set; }
	private List<RefreshSession> _refreshSessions;
	public IReadOnlyList<RefreshSession> RefreshSessions => _refreshSessions.AsReadOnly();

	private User(string name, string hashedPassword)
	{
		Id = new UserId(Guid.NewGuid());
		Name = name;
		HashedPassword = hashedPassword;
		_refreshSessions = new List<RefreshSession>();
	}

	public static User Create(string name, string hashedPassword)
	{
		var user = new User(name, hashedPassword);

		user.AddDomainEvent(new UserCreated(user));

		return user;
	}

	public void AddRefreshSession(RefreshSession refreshSession)
	{
		_refreshSessions.Add(refreshSession);
		AddDomainEvent(new RefreshSessionCreated(refreshSession.RefreshToken, Id));
	}


	public bool TryRefreshSession(string ip, string oldRefreshToken, string newRefreshToken)
	{
		var thisSession = _refreshSessions.Find(rs => rs.Ip == ip);

		if (thisSession == null
			|| thisSession.RefreshToken != oldRefreshToken)
		{
			_refreshSessions.Clear();
			AddDomainEvent(new RefreshSessionUpdateFailed(ip, oldRefreshToken));
			return false;
		}

		thisSession.UpdateRefreshToken(newRefreshToken);

		AddDomainEvent(new RefreshSessionUpdated(ip, oldRefreshToken));
		return true;
	}
}
