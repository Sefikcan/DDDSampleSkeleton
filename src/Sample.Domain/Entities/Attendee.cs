using Sample.Domain.Primitives;

namespace Sample.Domain.Entities;

public sealed class Attendee : Entity
{
    private Attendee() {}

    internal Attendee(Guid gatheringId, Guid memberId, DateTime createdOnUtc)
    {
        GatheringId = gatheringId;
        MemberId = memberId;
        CreatedOnUtc = createdOnUtc;
    }
    
    public Guid GatheringId { get; private set; }
    
    public Guid MemberId { get; private set; }
    
    public DateTime CreatedOnUtc { get; private set; }
}