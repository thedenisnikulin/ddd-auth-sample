using Infrastructure.Data;
using Manga.Application.Contracts;
using Manga.Domain.Entities;
using SharedKernel;

namespace Infrastructure.Manga.Repositories;

public class AuthorRepository : IAuthorRepository
{
	private readonly AppDbContext _context;

	public AuthorRepository(AppDbContext context)
	{
		_context = context;
	}

	public void Add(Author author)
	{
		_context.Authors.Add(author);
	}

	public Author? GetById(AuthorId authorId)
	{
		return _context.Authors.FirstOrDefault(a => a.Id == authorId);
	}

	public void Remove(Author author)
	{
		_context.Authors.Remove(author);
	}

	public void Save()
	{
		_context.SaveChanges();
	}
}
