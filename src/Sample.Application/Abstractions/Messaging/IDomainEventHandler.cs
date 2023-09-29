using MediatR;
using Sample.Domain.Primitives;

namespace Sample.Application.Abstractions.Messaging;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> 
    where TEvent : IDomainEvent
{
}