using Sample.Domain.Enums;
using Sample.Domain.Primitives;

namespace Sample.Domain.Entities;

public sealed class Invitation : Entity
{
    internal Invitation(Guid id, Guid memberId, Guid gatheringId, InvitationStatus invitationStatus) : base(id)
    {
        MemberId = memberId;
        GatheringId = gatheringId;
        InvitationStatus = invitationStatus;
        CreatedOnUtc = DateTime.UtcNow;
    }
    
    private Invitation() {}
    
    public Guid MemberId { get; private set; }
    
    public Guid GatheringId { get; private set; }
    
    public InvitationStatus InvitationStatus { get; private set; }
    
    public DateTime CreatedOnUtc { get; private set; }
    
    public DateTime ModifiedOnUtc { get; private set; }
}