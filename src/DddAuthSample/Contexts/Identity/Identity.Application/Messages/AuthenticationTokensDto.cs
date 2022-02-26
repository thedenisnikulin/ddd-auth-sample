namespace Identity.Application.Messages;

public record AuthenticationTokensDto(string AccessToken, string RefreshToken);
