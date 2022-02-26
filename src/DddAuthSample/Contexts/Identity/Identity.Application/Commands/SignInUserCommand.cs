using MediatR;
using Identity.Application.Options;
using Identity.Application.Messages;
using Identity.Application.Exceptions;
using Identity.Application.Common;
using Identity.Domain.Entities;
using Identity.Application.Contracts;
using Microsoft.Extensions.Options;

namespace Identity.Application.Commands;

public class SignInUserCommand : IRequest<Result<AuthenticationTokensDto>>
{
	public string UserName { get; set; }
	public string Password { get; set; }
	public string Ip { get; set; }

	public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, Result<AuthenticationTokensDto>>
	{
		private readonly IUserService _userService;
		private readonly IEncryptionService _encryptionService;
		private readonly ITokenFactory _tokenFactory;
		private readonly RefreshSessionOptions _refreshSessionOptions;

		public SignInUserCommandHandler(
			IUserService userService,
			IEncryptionService encryptionService,
			ITokenFactory tokenFactory,
			IOptions<RefreshSessionOptions> refreshSessionOptions)
		{
			_userService = userService;
			_encryptionService = encryptionService;
			_tokenFactory = tokenFactory;
			_refreshSessionOptions = refreshSessionOptions.Value;
		}

		public async Task<Result<AuthenticationTokensDto>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userService.GetByName(request.UserName);
			var isPasswordVerified = _encryptionService.VerifyPassword(user.HashedPassword, request.Password);

			if (!isPasswordVerified)
			{
				return Result<AuthenticationTokensDto>.Fail();
			}
			
			var dto = new AuthenticationTokensDto(
				_tokenFactory.GenerateAccessToken(user),
				_tokenFactory.GenerateRefreshToken());

			// better configure expiration date via injected options
			user.AddRefreshSession(
					RefreshSession.Create(
						user.Id,
						dto.RefreshToken,
						request.Ip,
						DateTime.Now.AddDays(_refreshSessionOptions.ValidForInDays)));

			await _userService.Update(user);

			return Result<AuthenticationTokensDto>.Success(dto);
		}
	}
}
