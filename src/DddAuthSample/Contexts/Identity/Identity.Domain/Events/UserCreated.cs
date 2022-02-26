using SeedWork;
using Identity.Domain.Entities;

namespace Identity.Domain.Events;

public class UserCreated : IDomainEvent
{
	public User User { get; }

	public UserCreated(User user)
	{
		User = user;
	}
}
