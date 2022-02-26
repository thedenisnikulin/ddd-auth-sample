using SeedWork;
using Manga.Domain.Entities;

namespace Manga.Domain.Events.Author;

public class MangaRemoved : IDomainEvent
{
	public MangaId MangaId { get; }

	public MangaRemoved(MangaId mangaId)
	{
		MangaId = mangaId;
	}
}
