using Manga.Application.Contracts;
using Manga.Domain.Entities;
using MediatR;

namespace Manga.Application.Commands;

public class ChangeMangaCommand : IRequest<Unit>
{
	public Guid AuthorId { get; set; }
	public Guid MangaId { get; set; }
	public string NewMangaTitle { get; set; }

	public class ChangeMangaCommandHandler : IRequestHandler<ChangeMangaCommand, Unit>
	{
		private readonly IAuthorRepository _authorRepository;
		private readonly IMangaRepository _mangaRepository;

		public ChangeMangaCommandHandler(
			IAuthorRepository authorRepository,
			IMangaRepository mangaRepository)
		{
			_authorRepository = authorRepository;
			_mangaRepository = mangaRepository;
		}

		public Task<Unit> Handle(ChangeMangaCommand request, CancellationToken cancellationToken)
		{
			var authorId = new AuthorId(request.AuthorId);
			var mangaId = new MangaId(request.MangaId);

			var author = _authorRepository.GetById(authorId);
			var manga = _mangaRepository.GetById(mangaId);
		
			author.ChangePublishedMangaTitle(manga, request.NewMangaTitle);

			_authorRepository.Update(author);
			_authorRepository.Save();
			return Unit.Task;
		}
	}
}
