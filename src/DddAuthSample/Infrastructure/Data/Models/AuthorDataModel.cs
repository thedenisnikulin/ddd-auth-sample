namespace Infrastructure.Data.Models;

public class AuthorDataModel
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public List<MangaDataModel> PublishedManga { get; set; }
}
