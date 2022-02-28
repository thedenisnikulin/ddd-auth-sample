using Infrastructure.Data;
using Manga.Application.Contracts;
using Manga.Domain.Entities;
using SharedKernel;
using Infrastructure.Data.Models;
using AutoMapper;

namespace Infrastructure.Manga.Repositories;

public class ReaderRepository : IReaderRepository
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public ReaderRepository(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public void Add(Reader reader)
	{
		var readerDataModel = _mapper.Map<ReaderDataModel>(reader);
		_context.Readers.Add(readerDataModel);
	}

	public Reader? GetById(ReaderId readerId)
	{
		var reader = _context.Readers.FirstOrDefault(a => a.Id == _mapper.Map<Guid>(readerId));
		return _mapper.Map<Reader>(reader);
	}

	public void Remove(Reader reader)
	{
		var readerDataModel = _mapper.Map<ReaderDataModel>(reader);
		_context.Readers.Remove(readerDataModel);
	}


	public void Save()
	{
		_context.SaveChanges();
	}
}
