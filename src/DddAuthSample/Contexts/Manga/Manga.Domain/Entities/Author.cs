using SeedWork;
using SharedKernel;
using Manga.Domain.Events.Author;

namespace Manga.Domain.Entities;

public class Author : Entity, IAggregateRoot
{
	public AuthorId Id { get; }
	public UserId UserId { get; }
	private List<Manga> _publishedManga;
	public IReadOnlyCollection<Manga> PublishedManga => _publishedManga.AsReadOnly();

	private Author() {}

	private Author(UserId userId)
	{
		Id = new AuthorId(Guid.NewGuid());
		UserId = userId;
		_publishedManga = new List<Manga>();
	}

	public static Author Create(UserId userId)
	{
		var author = new Author(userId);

		author.AddDomainEvent(new AuthorCreated(author.Id, userId));
		return author;
	}

	public void PublishManga(Manga manga)
	{
		if (_publishedManga.Contains(manga))
		{
			throw new Exception(); // TODO
		}
		_publishedManga.Add(manga);

		AddDomainEvent(new MangaPublished(Id, manga.Id));
	}

	public void ChangePublishedMangaTitle(Manga manga, string title)
	{
		var mangaToChange = _publishedManga.Find(m => m.Id == manga.Id);
		if (mangaToChange == null)
		{
			throw new Exception(); // TODO
		}
		mangaToChange.ChangeMangaTitle(title);

		AddDomainEvent(new MangaTitleChanged(manga.Id));
	}

	public void RemovePublishedManga(Manga manga)
	{
		if (!_publishedManga.Contains(manga))
		{
			throw new Exception(); // TODO
		}
		_publishedManga.Remove(manga);

		AddDomainEvent(new MangaRemoved(manga.Id));
	}
}
