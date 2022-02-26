using SeedWork;
using Manga.Domain.Entities;

namespace Manga.Domain.Events.Reader;

public class MangaBookmarked : IDomainEvent
{
	public ReaderId ReaderId { get; }
	public MangaId MangaId { get; }

	public MangaBookmarked(ReaderId readerId, MangaId mangaId)
	{
		ReaderId = readerId;
		MangaId = mangaId;
	}
}
