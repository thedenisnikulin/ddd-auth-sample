using SeedWork;

namespace Manga.Domain.Entities;

public readonly record struct AuthorId(Guid Value) : ITypedId<Guid>;
