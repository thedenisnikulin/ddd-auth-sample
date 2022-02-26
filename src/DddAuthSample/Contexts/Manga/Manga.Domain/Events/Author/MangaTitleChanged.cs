using SeedWork;
using Manga.Domain.Entities;

namespace Manga.Domain.Events.Author;

public class MangaTitleChanged : IDomainEvent
{
	public MangaId MangaId { get; }

	public MangaTitleChanged(MangaId mangaId)
	{
		MangaId = mangaId;
	}
}
