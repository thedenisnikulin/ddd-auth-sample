using SeedWork;

namespace SharedKernel;

public readonly record struct UserId(Guid Value) : ITypedId<Guid>;
