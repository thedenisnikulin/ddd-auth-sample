using Manga.Domain.Entities;
using SharedKernel;

namespace Manga.Application.Contracts;

public interface IReaderRepository
{
	Reader? GetById(ReaderId readerId);
	void Remove(Reader reader);
	void Add(Reader reader);
	void Save();
}
