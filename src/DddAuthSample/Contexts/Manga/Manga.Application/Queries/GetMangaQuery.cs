using MediatR;
using Manga.Application.Contracts;
using Manga.Domain.Entities;

namespace Manga.Application.Queries;

using Manga = Manga.Domain.Entities.Manga;

public class GetMangaQuery : IRequest<Manga>
{
	public Guid MangaId { get; set; }

	public class GetMangaQueryHandler : IRequestHandler<GetMangaQuery, Manga>
	{
		private readonly IMangaRepository _mangaRepository;

		public GetMangaQueryHandler(IMangaRepository mangaRepository)
		{
			_mangaRepository = mangaRepository;
		}

		public Task<Manga> Handle(GetMangaQuery request, CancellationToken cancellationToken)
		{
			var mangaId = new MangaId(request.MangaId);
			var manga = _mangaRepository.GetById(mangaId);
			return Task.FromResult(manga);
		}
	}
}
