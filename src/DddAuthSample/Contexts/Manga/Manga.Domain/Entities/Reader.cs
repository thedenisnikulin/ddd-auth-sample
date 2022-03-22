using SeedWork;
using SharedKernel;
using Manga.Domain.Events.Reader;

namespace Manga.Domain.Entities;

public class Reader : Entity, IAggregateRoot
{
	public ReaderId Id { get; private set; }
	public UserId UserId { get; private set; }
	private List<BookmarkedManga> _bookmarkedManga;
	public IReadOnlyCollection<BookmarkedManga> BookmarkedManga
	{
		get => _bookmarkedManga.AsReadOnly();
		set => _bookmarkedManga = value.ToList();
	}

	private Reader() {}

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

	public void AddMangaToBookmarks(MangaId mangaId, Bookmark bookmark)
	{
		_bookmarkedManga.Add(Domain.Entities.BookmarkedManga.Create(mangaId, bookmark, Id));

		AddDomainEvent(new MangaBookmarked(Id, mangaId));
	}

	public void ChangeBookmarkedManga(MangaId mangaId, Bookmark bookmark)
	{
		var mangaToChange = _bookmarkedManga.Find(bm => bm.MangaId == mangaId);
		mangaToChange?.ChangeBookmark(bookmark);

		AddDomainEvent(new BookmarkedMangaChanged(Id, mangaId));
	}

	public void RemoveBookmarkedManga(MangaId mangaId)
	{
		var mangaToRemove = _bookmarkedManga.Find(bm => bm.MangaId == mangaId);
		_bookmarkedManga.Remove(mangaToRemove);

		AddDomainEvent(new BookmarkedMangaRemoved(Id, mangaId));
	}
}
