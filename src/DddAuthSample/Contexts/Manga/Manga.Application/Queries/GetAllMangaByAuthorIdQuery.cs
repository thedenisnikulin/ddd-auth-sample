using MediatR;
using Manga.Domain.Entities;
using Manga.Application.Contracts;

namespace Manga.Application.Queries;

using Manga = Manga.Domain.Entities.Manga;

public class GetAllMangaByAuthorIdQuery : IRequest<IEnumerable<Manga>>
{
	public Guid AuthorId { get; set; }

	public class GetAllMangaByAuthorIdQueryHandler : IRequestHandler<GetAllMangaByAuthorIdQuery, IEnumerable<Manga>>
	{
		private readonly IMangaRepository _mangaRepository;

		public GetAllMangaByAuthorIdQueryHandler(IMangaRepository mangaRepository)
		{
			_mangaRepository = mangaRepository;
		}

		public Task<IEnumerable<Manga>> Handle(GetAllMangaByAuthorIdQuery request, CancellationToken cancellationToken)
		{
			var authorId = new AuthorId(request.AuthorId);
			var allManga = _mangaRepository.GetAllByAuthorId(authorId);
			return Task.FromResult(allManga);
		}
	}
}
