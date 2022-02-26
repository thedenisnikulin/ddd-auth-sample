using System.Collections.Generic;

namespace SeedWork;

public abstract class Entity
{
	private List<IDomainEvent> _domainEvents = new();
	public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

	protected void AddDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}

	public void ClearDomainEvents()
	{
		_domainEvents?.Clear();
	}
}
