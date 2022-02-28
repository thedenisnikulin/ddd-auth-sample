using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Manga.Application.Contracts;
using Manga.Domain.Entities;
using SharedKernel;

namespace Infrastructure.Manga.Repositories;

public class AuthorRepository : IAuthorRepository
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public AuthorRepository(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public void Add(Author author)
	{
		var authorDataModel = _mapper.Map<AuthorDataModel>(author);
		_context.Authors.Add(authorDataModel);
	}

	public Author? GetById(AuthorId authorId)
	{
		var author = _context.Authors.FirstOrDefault(a => a.Id == _mapper.Map<Guid>(authorId));
		return _mapper.Map<Author>(author);
	}

	public void Remove(Author author)
	{
		var authorDataModel = _mapper.Map<AuthorDataModel>(author);
		_context.Authors.Remove(authorDataModel);
	}

	public void Save()
	{
		_context.SaveChanges();
	}
}
