using System.Reflection;
using System.Reflection.Emit;
using Sample.Domain.Enums;
using Sample.Domain.Exceptions;
using Sample.Domain.Primitives;

namespace Sample.Domain.Entities;

public sealed class Gathering : AggregateRoot
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();

    private Gathering(Guid id, Member member, GatheringType gatheringType, string name, DateTime scheduledAtUtc, string location) : base(id)
    {
        Member = member;
        GatheringType = gatheringType;
        Name = name;
        ScheduledAtUtc = scheduledAtUtc;
        Location = location;
    }
    
    private Gathering(){} // EF Core
    
    public Member Member { get; private set; }
    
    public GatheringType GatheringType { get; private set; }

    public string Name { get; private set; }

    public DateTime ScheduledAtUtc { get; private set; }

    public string Location { get; private set; }

    public int? MaximumNumberOfAttendees { get; private set; }

    public DateTime InvitationsExpireAtUtc { get; private set; }

    public int NumberOfAttendees { get; private set; }

    public bool Cancelled { get; private set; }

    public IReadOnlyCollection<Attendee> Attendees()
    {
        return _attendees;
    }

    public IReadOnlyCollection<Invitation> Invitations()
    {
        return _invitations;
    }

    public static Gathering Create(Guid id, 
        Member member, 
        GatheringType type, 
        string name,
        DateTime scheduledAtUtc, 
        string location, 
        int? maximumNumberOfAttendees, 
        int? invitationsValidBeforeInHours)
    {
        Gathering gathering = new Gathering(id, member, type, name, scheduledAtUtc, location);
        gathering.CalculateGatheringTypeDetails(maximumNumberOfAttendees, invitationsValidBeforeInHours);

        return gathering;
    }

    private void CalculateGatheringTypeDetails(int? maximumNumberOfAttendees, int? invitationsValidBeforeInHours)
    {
        switch (GatheringType)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
                {
                    throw new GatheringMaximumNumberOfAttendeesIsNullDomainException($"{nameof(maximumNumberOfAttendees)} can't be null.");
                }

                MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;
            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new GatheringInvitationsValidBeforeInHoursIsNullDomainException(
                        $"{nameof(invitationsValidBeforeInHours)} can't be null.");
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }
    }
}