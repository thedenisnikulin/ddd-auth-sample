using Manga.Domain.Entities;
using SharedKernel;

namespace Manga.Application.Contracts;

public interface IAuthorRepository
{
	Author? GetById(AuthorId author);
	void Add(Author author);
	void Update(Author author);
	void Remove(Author author);
	void Save();
}
