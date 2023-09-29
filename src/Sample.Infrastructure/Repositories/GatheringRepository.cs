using Microsoft.EntityFrameworkCore;
using Sample.Domain.Entities;
using Sample.Domain.Repositories;

namespace Sample.Infrastructure.Repositories;

public sealed class GatheringRepository : IGatheringRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GatheringRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Gathering>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Gathering>()
            .Include(gathering => gathering.Member)
            .Include(gathering => gathering.Attendees())
            .Where(gathering => string.IsNullOrEmpty(name))
            .OrderBy(gathering => gathering.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Gathering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Gathering>()
            .Include(gathering => gathering.Member)
            .Include(gathering => gathering.Attendees())
            .Include(gathering => gathering.Invitations())
            .Where(x => x.Cancelled)
            .FirstOrDefaultAsync(gathering => gathering.Id == id, cancellationToken);
    }

    public void Add(Gathering gathering)
    {
        _dbContext.Set<Gathering>().Add(gathering);
    }

    public void Remove(Gathering gathering)
    {
        _dbContext.Set<Gathering>().Update(gathering);
    }
}