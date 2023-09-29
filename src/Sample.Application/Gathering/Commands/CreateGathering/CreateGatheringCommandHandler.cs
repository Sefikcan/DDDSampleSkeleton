using MediatR;
using Sample.Domain.Entities;
using Sample.Domain.Repositories;

namespace Sample.Application.Gathering.Commands.CreateGathering;

internal sealed class CreateGatheringCommandHandler : IRequestHandler<CreateGatheringCommand, Unit>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGatheringCommandHandler(
        IMemberRepository memberRepository, 
        IGatheringRepository gatheringRepository, 
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
        if (member is null)
        {
            return Unit.Value;
        }

        Domain.Entities.Gathering gathering = Domain.Entities.Gathering.Create(
            Guid.NewGuid(),
            member,
            request.Type,
            request.Name,
            request.ScheduledAtUtc,
            request.Location,
            request.MaximumNumberOfAttendees,
            request.InvitationsValidBeforeInHours
        );
        
        _gatheringRepository.Add(gathering);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}