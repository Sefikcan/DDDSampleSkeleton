namespace Sample.Application.Gathering.Queries.GetGatheringById;

public sealed record AttendeeResponse(Guid MemberId, DateTime CreatedOnUtc);