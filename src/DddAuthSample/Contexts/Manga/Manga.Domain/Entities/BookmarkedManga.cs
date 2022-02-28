using SeedWork;

namespace Manga.Domain.Entities;

public enum Bookmark
{
	PlanningToRead,
	Reading,
	FinishedReading,
}

public class BookmarkedManga : Entity 
{
	public MangaId MangaId { get; }
	public Bookmark Bookmark { get; private set; }
	public ReaderId ReaderId { get; }

	private BookmarkedManga() {}

	private BookmarkedManga(MangaId mangaId, Bookmark bookmark, ReaderId readerId)
	{
		MangaId = mangaId;
		Bookmark = bookmark;
		ReaderId = readerId;
	}

	public static BookmarkedManga Create(MangaId mangaId, Bookmark bookmark, ReaderId readerId)
	{
		var bookmarkedManga = new BookmarkedManga(mangaId, bookmark, readerId);
		
		return bookmarkedManga;
	}

	public void ChangeBookmark(Bookmark bookmark)
	{
		Bookmark = bookmark;
	}
}
