using MediatR;
using Manga.Domain.Entities;
using Manga.Application.Contracts;

namespace Manga.Application.Queries;

public class GetAllMangaByReaderIdQuery : IRequest<IEnumerable<BookmarkedManga>>
{
	public Guid ReaderId { get; set; }

	public class GetAllMangaByReaderIdHandler : IRequestHandler<GetAllMangaByReaderIdQuery, IEnumerable<BookmarkedManga>>
	{
		private readonly IMangaRepository _mangaRepository;

		public GetAllMangaByReaderIdHandler(IMangaRepository mangaRepository)
		{
			_mangaRepository = mangaRepository;
		}

		public Task<IEnumerable<BookmarkedManga>> Handle(GetAllMangaByReaderIdQuery request, CancellationToken cancellationToken)
		{
			var readerId = new ReaderId(request.ReaderId);
			var allManga = _mangaRepository.GetAllByReaderId(readerId);
			return Task.FromResult(allManga);
		}
	}
}
