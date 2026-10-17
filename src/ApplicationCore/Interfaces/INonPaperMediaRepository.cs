using Domain.Models.NPMs;

namespace ApplicationCore.Interfaces;

public interface INonPaperMediaRepository
{
    void Insert(NonPaperMedia nonPaperMedia);
    Task<List<NonPaperMedia>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<NonPaperMedia?> GetByIdAsync(NpmId id, CancellationToken cancellationToken = default);
}
