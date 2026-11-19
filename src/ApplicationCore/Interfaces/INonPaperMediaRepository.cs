using Domain.Models.NPMs;

namespace ApplicationCore.Interfaces;

public interface INonPaperMediaRepository
{
    void Insert(NonPaperMedia nonPaperMedia);
    Task<List<NonPaperMedia>> GetAll(CancellationToken cancellationToken = default);
    Task<NonPaperMedia?> GetById(NpmId id, CancellationToken cancellationToken = default);
    Task<List<NonPaperMedia>> GetByIds(IEnumerable<NpmId> ids, CancellationToken cancellationToken = default);
    Task<bool> IsAvailable(NpmId id, CancellationToken cancellationToken = default);
    Task<bool> HasNpmBySerialnumber(string serialnumber, CancellationToken cancellationToken = default);
}
