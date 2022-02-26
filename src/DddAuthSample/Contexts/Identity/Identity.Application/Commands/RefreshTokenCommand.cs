using MediatR;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Identity.Application.Common;
using Identity.Application.Messages;
using SharedKernel;

namespace Identity.Application.Commands;

public class RefreshTokenCommand : IRequest<Result<AuthenticationTokensDto>>
{
	public UserId UserId { get; set; }
	public string Ip { get; set; }
	public string RefreshToken { get; set; }

	public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthenticationTokensDto>>
	{
		private readonly IUserService _userService;
		private readonly ITokenFactory _tokenFactory;

		public RefreshTokenCommandHandler(
			IUserService userService,
			ITokenFactory tokenFactory)
		{
			_userService = userService;
			_tokenFactory = tokenFactory;
		}

		public async Task<Result<AuthenticationTokensDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var user = await _userService.GetById(request.UserId);
			var newRefreshToken = _tokenFactory.GenerateRefreshToken();

			var hasRefreshed = user.TryRefreshSession(request.Ip, request.RefreshToken, newRefreshToken);

			await _userService.Update(user);

			if (!hasRefreshed)
			{
				return Result<AuthenticationTokensDto>.Fail();
			}

			var dto = new AuthenticationTokensDto(_tokenFactory.GenerateAccessToken(user), newRefreshToken);

			return Result<AuthenticationTokensDto>.Success(dto);
		}
	}
}
