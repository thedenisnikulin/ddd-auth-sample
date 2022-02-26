using SeedWork;

namespace Manga.Domain.Entities;

public readonly record struct MangaId(Guid Value) : ITypedId<Guid>;
