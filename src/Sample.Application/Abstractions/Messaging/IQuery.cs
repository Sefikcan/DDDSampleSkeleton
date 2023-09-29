using MediatR;
using Sample.Domain.Shared;

namespace Sample.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}