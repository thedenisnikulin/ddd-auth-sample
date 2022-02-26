using SeedWork;
using SharedKernel;
using Manga.Domain.Entities;

namespace Manga.Domain.Events.Author;

public class AuthorCreated : IDomainEvent
{
	public AuthorId AuthorId { get; }
	public UserId UserId { get; }

	public AuthorCreated(AuthorId authorId, UserId userId)
	{
		AuthorId = authorId;
		UserId = userId;
	}
}
