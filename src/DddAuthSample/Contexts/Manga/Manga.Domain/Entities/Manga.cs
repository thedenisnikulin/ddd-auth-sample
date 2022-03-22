using SeedWork;
using Identity.Domain.Entities;

namespace Manga.Domain.Entities;

public class Manga : Entity, IAggregateRoot
{
	public MangaId Id { get; private set; }
	public string Title { get; private set; }
	public AuthorId AuthorId { get; private set; }

	private Manga() {}

	private Manga(string title, AuthorId authorId)
	{
		Id = new MangaId(Guid.NewGuid());
		Title = title;
		AuthorId = authorId;
	}

	public static Manga CreateManga(string title, AuthorId authorId)
	{
		var manga = new Manga(title, authorId);

		return manga;
	}

	public void ChangeMangaTitle(string title)
	{
		Title = title;
	}
}
