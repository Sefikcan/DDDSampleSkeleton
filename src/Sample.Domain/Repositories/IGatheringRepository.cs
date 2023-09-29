using Sample.Domain.Entities;

namespace Sample.Domain.Repositories;

public interface IGatheringRepository
{
    Task<List<Gathering>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<Gathering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    void Add(Gathering gathering);

    void Remove(Gathering gathering);
}