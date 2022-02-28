namespace Infrastructure.Data.Models;

public class MangaDataModel
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public Guid AuthorId { get; set; }
}
