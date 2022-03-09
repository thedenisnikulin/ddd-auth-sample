using MediatR;
using Identity.Application.Contracts;
using Identity.Domain.Entities;

namespace Identity.Application.Commands;

public class RegisterUserCommand : IRequest
{
	public string UserName { get; set; }
	public string Password { get; set; }

	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
	{
		private readonly IUserService _userService;

		public RegisterUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			// There used to be a domain service for checking uniqueness of the username,
			// but I decided that it's the responsibility of persistance layer,
			// so the persistance exception will be raised if name is not unique

			var user = User.Create(request.UserName, request.Password);

			await _userService.Create(user);

			return Unit.Value;
		}
	}
}
