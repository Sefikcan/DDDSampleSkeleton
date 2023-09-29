using Sample.Domain.Primitives;

namespace Sample.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;