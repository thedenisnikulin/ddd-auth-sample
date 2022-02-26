using MediatR;
using Manga.Domain.Entities;
using Manga.Application.Contracts;

namespace Manga.Application.Queries;

using Manga = Manga.Domain.Entities.Manga;

public class GetAllMangaPagedQuery : IRequest<IEnumerable<Manga>>
{
	public int MangaPerPage { get; set; }

	public class GetAllMangaPagedQueryHandler : IRequestHandler<GetAllMangaPagedQuery, IEnumerable<Manga>>
	{
		private readonly IMangaRepository _mangaRepository;

		public GetAllMangaPagedQueryHandler(IMangaRepository mangaRepository)
		{
			_mangaRepository = mangaRepository;
		}

		public Task<IEnumerable<Manga>> Handle(GetAllMangaPagedQuery request, CancellationToken cancellationToken)
		{
			var allManga = _mangaRepository.GetAllPaged(request.MangaPerPage);
			return Task.FromResult(allManga);
		}
	}
}
