using Sample.Application.Abstractions.Messaging;
using Sample.Domain.Errors;
using Sample.Domain.Repositories;
using Sample.Domain.Shared;

namespace Sample.Application.Gathering.Queries.GetGatheringById;

internal sealed class GetGatheringByIdQueryHandler : IQueryHandler<GetGatheringByIdQuery, GatheringResponse>
{
    private readonly IGatheringRepository _gatheringRepository;

    public GetGatheringByIdQueryHandler(IGatheringRepository gatheringRepository)
    {
        _gatheringRepository = gatheringRepository;
    }

    public async Task<Result<GatheringResponse>> Handle(GetGatheringByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.Gathering? gathering = await _gatheringRepository.GetByIdAsync(request.Id, cancellationToken);
        if (gathering is null)
        {
            return Result.Failure<GatheringResponse>(DomainErrors.Gathering.NotFound(request.Id));
        }

        return new GatheringResponse(
            gathering.Id,
            gathering.Name,
            gathering.Location,
            $"{gathering.Member.FirstName.Value}" + $"{gathering.Member.LastName.Value}",
            gathering.Attendees().Select(attendee => new AttendeeResponse(
                attendee.MemberId,
                attendee.CreatedOnUtc
                )).ToList(),
            gathering.Invitations()
                .Select(invitation => new InvitationResponse(
                    invitation.Id,
                    invitation.InvitationStatus
                    )).ToList()
            );
    }
}