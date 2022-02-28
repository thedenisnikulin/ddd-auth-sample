using Manga.Application.Contracts;
using Manga.Domain.Entities;
using MediatR;

namespace Manga.Application.Commands;

public class BookmarkMangaCommand : IRequest<Unit>
{
	public Guid ReaderId { get; set; }
	public Guid MangaId { get; set; }
	public string Bookmark { get; set; }

	public class BookmarkMangaCommandHandler : IRequestHandler<BookmarkMangaCommand, Unit>
	{
		private readonly IReaderRepository _readerRepository;
		private readonly IMangaRepository _mangaRepository;

		public BookmarkMangaCommandHandler(
			IReaderRepository readerRepository,
			IMangaRepository mangaRepository)
		{
			_readerRepository = readerRepository;
			_mangaRepository = mangaRepository;
		}

		public Task<Unit> Handle(BookmarkMangaCommand request, CancellationToken cancellationToken)
		{
			var readerId = new ReaderId(request.ReaderId);
			var mangaId = new MangaId(request.MangaId);

			var reader = _readerRepository.GetById(readerId);
			
			reader.AddMangaToBookmarks(mangaId, (Bookmark) Enum.Parse<Bookmark>(request.Bookmark));

			_readerRepository.Save();
			return Unit.Task;
		}
	}
}
