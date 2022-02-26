using SeedWork;

namespace Manga.Domain.Entities;

public readonly record struct ReaderId(Guid Value) : ITypedId<Guid>;
