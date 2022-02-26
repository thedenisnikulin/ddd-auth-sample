using SeedWork;
using Manga.Domain.Entities;

namespace Manga.Domain.Events.Reader;

public class BookmarkedMangaChanged : IDomainEvent
{
	public ReaderId ReaderId { get; }
	public MangaId MangaId { get; }

	public BookmarkedMangaChanged(ReaderId readerId, MangaId mangaId)
	{
		ReaderId = readerId;
		MangaId = mangaId;
	}
}
