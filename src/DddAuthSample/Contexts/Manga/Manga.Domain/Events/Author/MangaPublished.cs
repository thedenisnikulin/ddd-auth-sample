using SeedWork;
using Manga.Domain.Entities;

namespace Manga.Domain.Events.Author;

public class MangaPublished : IDomainEvent
{
	public AuthorId AuthorId { get; }
	public MangaId MangaId { get; }

	public MangaPublished(AuthorId authorId, MangaId mangaId)
	{
		AuthorId = authorId;
		MangaId = mangaId;
	}
}
