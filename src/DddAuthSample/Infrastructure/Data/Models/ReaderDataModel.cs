namespace Infrastructure.Data.Models;

public class ReaderDataModel
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public List<BookmarkedMangaDataModel> BookmarkedManga { get; set; }
}
