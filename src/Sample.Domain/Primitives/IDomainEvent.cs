using MediatR;

namespace Sample.Domain.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}