namespace Infrastructure.Data.Models;

public class RefreshSessionDataModel
{
	public Guid UserId { get; set; }
	public string RefreshToken { get; set; }
	public string Ip { get; set; }
	public DateTime ExpiresAt { get; set; }
	public DateTime CreatedAt { get; set; }
}
