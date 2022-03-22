using Manga.Domain.Entities;
using SharedKernel;

namespace Manga.Application.Contracts;

public interface IReaderRepository
{
	Reader? GetById(ReaderId readerId);
	void Add(Reader reader);
	void Update(Reader reader);
	void Remove(Reader reader);
	void Save();
}
