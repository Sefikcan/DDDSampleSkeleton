using Microsoft.EntityFrameworkCore;
using Sample.Domain.Entities;
using Sample.Domain.Repositories;
using Sample.Domain.ValueObjects.Member;

namespace Sample.Infrastructure.Repositories;

public sealed class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MemberRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Member>().FirstOrDefaultAsync(member => member.Id == id, cancellationToken);
    }

    public async Task<List<Member>> GetMembersAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Member>().ToListAsync(cancellationToken);
    }

    public void Add(Member member)
    {
        _dbContext.Set<Member>().Add(member);
    }

    public void Update(Member member)
    {
        _dbContext.Set<Member>().Update(member);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        return !await _dbContext.Set<Member>().AnyAsync(member => member.Email == email, cancellationToken);
    }
}