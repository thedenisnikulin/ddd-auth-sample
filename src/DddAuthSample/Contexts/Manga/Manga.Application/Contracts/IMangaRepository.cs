using Manga.Domain.Entities;

namespace Manga.Application.Contracts;

using Manga = Manga.Domain.Entities.Manga;

public interface IMangaRepository
{
	Manga? GetById(MangaId id);
	IEnumerable<Manga> GetAllByAuthorId(AuthorId authorId);
	IEnumerable<BookmarkedManga> GetAllByReaderId(ReaderId readerId);
	IEnumerable<Manga> GetAllPaged(int mangaPerPage);
	void Add(Manga manga);
	void Remove(Manga manga);
	void Save();
}
