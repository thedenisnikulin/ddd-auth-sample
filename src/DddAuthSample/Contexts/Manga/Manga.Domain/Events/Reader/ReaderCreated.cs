using SeedWork;
using SharedKernel;
using Manga.Domain.Entities;

namespace Manga.Domain.Events.Reader;

public class ReaderCreated : IDomainEvent
{
	public ReaderId ReaderId { get; }
	public UserId UserId { get; }

	public ReaderCreated(ReaderId readerId, UserId userId)
	{
		ReaderId = readerId;
		UserId = userId;
	}
}
