namespace Infrastructure.Data.Models;

public class BookmarkedMangaDataModel
{
	public Guid MangaId { get; set; }
	public string Bookmark { get; set; }
	public Guid ReaderId { get; set; }
}
