using Sample.Application.Abstractions.Messaging;

namespace Sample.Application.Gathering.Queries.GetGatheringById;

public sealed record GetGatheringByIdQuery(Guid Id) : IQuery<GatheringResponse>;