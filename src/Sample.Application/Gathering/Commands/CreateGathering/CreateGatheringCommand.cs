using MediatR;
using Sample.Domain.Enums;

namespace Sample.Application.Gathering.Commands.CreateGathering;

public sealed record CreateGatheringCommand(
    Guid MemberId,
    GatheringType Type,
    DateTime ScheduledAtUtc,
    string Name,
    string Location,
    int? MaximumNumberOfAttendees,
    int? InvitationsValidBeforeInHours
    ) : IRequest<Unit>;