using Domain.Models.NPMs;

namespace ApplicationCore.Interfaces;

public interface INonPaperMediaRepository
{
    void Insert(NonPaperMedia nonPaperMedia);
    Task<List<NonPaperMedia>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<NonPaperMedia?> GetByIdAsync(NpmId id, CancellationToken cancellationToken = default);
    Task<List<NonPaperMedia>> GetByIdsAsync(IEnumerable<NpmId> ids, CancellationToken cancellationToken = default);
    Task<bool> IsAvailableAsync(NpmId id, CancellationToken cancellationToken = default);
    Task<bool> HasNpmBySerialNumberAsync(string serialnumber, CancellationToken cancellationToken);
}
