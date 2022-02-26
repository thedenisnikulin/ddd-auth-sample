using Infrastructure.Data;
using Manga.Application.Contracts;
using Manga.Domain.Entities;
using SharedKernel;

namespace Infrastructure.Manga.Repositories;

public class ReaderRepository : IReaderRepository
{
	private readonly AppDbContext _context;

	public ReaderRepository(AppDbContext context)
	{
		_context = context;
	}

	public void Add(Reader reader)
	{
		_context.Readers.Add(reader);
	}

	public Reader? GetById(ReaderId readerId)
	{
		return _context.Readers.FirstOrDefault(r => r.Id == readerId);
	}

	public void Remove(Reader reader)
	{
		_context.Readers.Remove(reader);
	}

	public void Save()
	{
		_context.SaveChanges();
	}
}
