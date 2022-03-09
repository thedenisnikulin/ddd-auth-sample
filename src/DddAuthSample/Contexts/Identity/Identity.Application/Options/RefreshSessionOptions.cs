namespace Identity.Application.Options;

public class RefreshSessionOptions
{
	public const string RefreshSession = "RefreshSession";

	public int ValidForInDays { get; set; }
}
