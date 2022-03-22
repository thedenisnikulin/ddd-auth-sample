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

	public Author? GetById(AuthorId authorId)
	{
		var authorDataModel = _context.Authors.FirstOrDefault(a => a.Id == authorId.Value);
		return _mapper.Map<Author>(authorDataModel);
	}

	public void Add(Author author)
	{
		var authorDataModel = _mapper.Map<AuthorDataModel>(author);
		_context.Authors.Add(authorDataModel);
	}

	public void Update(Author author)
	{
		var updatedAuthorDataModel = _mapper.Map<AuthorDataModel>(author);
		var trackedAuthorDataModel = _context.Authors.Find(updatedAuthorDataModel.Id);

		_context.Entry(trackedAuthorDataModel).CurrentValues.SetValues(updatedAuthorDataModel);
		trackedAuthorDataModel.PublishedManga = updatedAuthorDataModel.PublishedManga;
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
