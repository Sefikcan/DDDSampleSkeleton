using Sample.Domain.Entities;
using Sample.Domain.ValueObjects.Member;

namespace Sample.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<List<Member>> GetMembersAsync(CancellationToken cancellationToken = default);

    void Add(Member member);

    void Update(Member member);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
}