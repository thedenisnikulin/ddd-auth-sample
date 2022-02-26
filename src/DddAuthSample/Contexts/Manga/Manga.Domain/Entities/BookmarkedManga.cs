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
	public Manga Manga { get; }
	public Bookmark Bookmark { get; private set; }
	public ReaderId ReaderId { get; }

	private BookmarkedManga(Manga manga, Bookmark bookmark, ReaderId readerId)
	{
		Manga = manga;
		Bookmark = bookmark;
		ReaderId = readerId;
	}

	public static BookmarkedManga Create(Manga manga, Bookmark bookmark, ReaderId readerId)
	{
		var bookmarkedManga = new BookmarkedManga(manga, bookmark, readerId);
		
		return bookmarkedManga;
	}

	public void ChangeBookmark(Bookmark bookmark)
	{
		Bookmark = bookmark;
	}
}
