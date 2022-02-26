using Manga.Domain.Entities;
using SharedKernel;

namespace Manga.Application.Contracts;

public interface IAuthorRepository
{
	Author? GetById(AuthorId author);
	void Remove(Author author);
	void Add(Author author);
	void Save();
}
