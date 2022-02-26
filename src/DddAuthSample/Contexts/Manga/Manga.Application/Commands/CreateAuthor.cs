using Manga.Domain.Entities;
using Manga.Application.Contracts;
using SharedKernel;
using MediatR;

namespace Manga.Application.Commands;

public class CreateAuthorCommand : IRequest<AuthorId>
{
	public Guid UserId { get; set; }

	public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorId>
	{
		private readonly IAuthorRepository _authorRepository;

		public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
		{
			_authorRepository = authorRepository;
		}

		public Task<AuthorId> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
		{
			var userId = new UserId(request.UserId);
			var author = Author.Create(userId);
			_authorRepository.Add(author);
			_authorRepository.Save();
			return Task.FromResult(author.Id);
		}
	}
}
