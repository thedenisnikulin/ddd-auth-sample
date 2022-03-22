using Manga.Application.Contracts;
using Manga.Domain.Entities;
using MediatR;

namespace Manga.Application.Commands;

using Manga = Manga.Domain.Entities.Manga;

public class PostMangaCommand : IRequest<Unit>
{
	public Guid AuthorId { get; set; }
	public string Title { get; set; }

	public class PostMangaCommandHandler : IRequestHandler<PostMangaCommand, Unit>
	{
		private readonly IAuthorRepository _authorRepository;

		public PostMangaCommandHandler(IAuthorRepository authorRepository)
		{
			_authorRepository = authorRepository;
		}

		public Task<Unit> Handle(PostMangaCommand request, CancellationToken cancellationToken)
		{
			var authorId = new AuthorId(request.AuthorId);
			var author = _authorRepository.GetById(authorId);

			var manga = Manga.CreateManga(request.Title, authorId);
			author.PublishManga(manga);

			_authorRepository.Update(author);
			_authorRepository.Save();
			return Unit.Task;
		}
	}
}
