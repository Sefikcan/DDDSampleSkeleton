using Sample.Domain.DomainEvents;
using Sample.Domain.Primitives;
using Sample.Domain.ValueObjects.Member;

namespace Sample.Domain.Entities;

public sealed class Member : AggregateRoot, IAuditableEntity
{
    private Member() {}

    private Member(Guid id, Email email, FirstName firstName, LastName lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
    
    public Email Email { get; private set; }
    
    public FirstName FirstName { get; private set; }
    
    public LastName LastName { get; private set; }
    
    public DateTime CreatedOnUtc { get; set; }
    
    public DateTime? ModifiedOnUtc { get; set; }

    public static Member Create(Guid id, Email email, FirstName firstName, LastName lastName)
    {
        Member member = new Member(id, email, firstName, lastName);
        
        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(Guid.NewGuid(), member.Id));

        return member;
    }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        if (!FirstName.Equals(firstName) || !LastName.Equals(lastName))
        {
            RaiseDomainEvent(new MemberNameChangedDomainEvent(Guid.NewGuid(), Id));
        }

        FirstName = firstName;
        LastName = lastName;
    }
}