using SeedWork;
using Manga.Domain.Entities;

namespace Manga.Domain.Events.Reader;

public class BookmarkedMangaRemoved : IDomainEvent
{
	public ReaderId ReaderId { get; }
	public MangaId MangaId { get; }

	public BookmarkedMangaRemoved(ReaderId readerId, MangaId mangaId)
	{
		ReaderId = readerId;
		MangaId = mangaId;
	}
}
