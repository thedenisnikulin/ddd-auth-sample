using Infrastructure.Data;
using Manga.Application.Contracts;
using Manga.Domain.Entities;
using DomainManga = Manga.Domain.Entities.Manga;
using Infrastructure.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Infrastructure.Manga.Repositories;

public class MangaRepository : IMangaRepository
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public MangaRepository(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public void Add(DomainManga manga)
	{
		var mangaDataModel = _mapper.Map<MangaDataModel>(manga);
		_context.Manga.Add(mangaDataModel);
	}

	public IEnumerable<DomainManga> GetAllByAuthorId(AuthorId authorId)
	{
		return _context.Manga
			.Where(m => m.AuthorId == _mapper.Map<Guid>(authorId))
			.ProjectTo<DomainManga>(_mapper.ConfigurationProvider);
	}

	public IEnumerable<BookmarkedManga> GetAllByReaderId(ReaderId readerId)
	{
		return _context.BookmarkedManga
			.Where(bm => bm.ReaderId == _mapper.Map<Guid>(readerId))
			.ProjectTo<BookmarkedManga>(_mapper.ConfigurationProvider);
	}

	public IEnumerable<DomainManga> GetAllPaged(int mangaPerPage)
	{
		return _context.Manga
			.Take(mangaPerPage)
			.ProjectTo<DomainManga>(_mapper.ConfigurationProvider);
	}

	public DomainManga? GetById(MangaId id)
	{
		var mangaDataModel = _context.Manga.FirstOrDefault(m => m.Id == _mapper.Map<Guid>(id));
		return _mapper.Map<DomainManga>(mangaDataModel);
	}

	public void Remove(DomainManga manga)
	{
		var mangaDataModel = _mapper.Map<MangaDataModel>(manga);
		_context.Manga.Remove(mangaDataModel);
	}

	public void Save()
	{
		_context.SaveChanges();
	}
}
