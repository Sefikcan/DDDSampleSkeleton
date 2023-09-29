using Sample.Domain.Enums;

namespace Sample.Application.Gathering.Queries.GetGatheringById;

public sealed record InvitationResponse(Guid Id, InvitationStatus InvitationStatus);