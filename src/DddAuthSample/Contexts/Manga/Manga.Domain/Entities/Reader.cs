using SeedWork;
using SharedKernel;
using Manga.Domain.Events.Reader;

namespace Manga.Domain.Entities;

public class Reader : Entity, IAggregateRoot
{
	public ReaderId Id { get; }
	public UserId UserId { get; }
	private List<BookmarkedManga> _bookmarkedManga { get; }
	public IReadOnlyCollection<BookmarkedManga> BookmarkedManga => _bookmarkedManga.AsReadOnly();

	private Reader(UserId userId)
	{
		Id = new ReaderId(Guid.NewGuid());
		UserId = userId;
		_bookmarkedManga = new List<BookmarkedManga>();
	}

	public static Reader Create(UserId userId)
	{
		var reader = new Reader(userId);

		reader.AddDomainEvent(new ReaderCreated(reader.Id, userId));
		return reader;
	}

	public void AddMangaToBookmarks(Manga manga, Bookmark bookmark)
	{
		_bookmarkedManga.Add(Domain.Entities.BookmarkedManga.Create(manga, bookmark, Id));

		AddDomainEvent(new MangaBookmarked(Id, manga.Id));
	}

	public void ChangeBookmarkedManga(Manga manga, Bookmark bookmark)
	{
		var mangaToChange = _bookmarkedManga.Find(bm => bm.Manga.Id == manga.Id);
		mangaToChange?.ChangeBookmark(bookmark);

		AddDomainEvent(new BookmarkedMangaChanged(Id, manga.Id));
	}

	public void RemoveBookmarkedManga(Manga manga)
	{
		var mangaToRemove = _bookmarkedManga.Find(bm => bm.Manga.Id == manga.Id);
		_bookmarkedManga.Remove(mangaToRemove);

		AddDomainEvent(new BookmarkedMangaRemoved(Id, manga.Id));
	}
}
