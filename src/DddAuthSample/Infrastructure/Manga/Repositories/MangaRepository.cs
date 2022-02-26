using Infrastructure.Data;
using Manga.Application.Contracts;
using Manga.Domain.Entities;
using DomainManga = Manga.Domain.Entities.Manga;

namespace Infrastructure.Manga.Repositories;

public class MangaRepository : IMangaRepository
{
	private readonly AppDbContext _context;

	public MangaRepository(AppDbContext context)
	{
		_context = context;
	}

	public void Add(DomainManga manga)
	{
		_context.Manga.Add(manga);
	}

	public IEnumerable<DomainManga> GetAllByAuthorId(AuthorId authorId)
	{
		return _context.Manga.Where(m => m.AuthorId == authorId);
	}

	public IEnumerable<BookmarkedManga> GetAllByReaderId(ReaderId readerId)
	{
		return _context.BookmarkedManga.Where(bm => bm.ReaderId == readerId);
	}

	public IEnumerable<DomainManga> GetAllPaged(int mangaPerPage)
	{
		return _context.Manga.Take(mangaPerPage);
	}

	public DomainManga? GetById(MangaId id)
	{
		return _context.Manga.FirstOrDefault(m => m.Id == id);
	}

	public void Remove(DomainManga manga)
	{
		_context.Manga.Remove(manga);
	}

	public void Save()
	{
		_context.SaveChanges();
	}
}
