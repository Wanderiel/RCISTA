using Domain.Models.NPMs;

namespace ApplicationCore.Interfaces;

public interface INonPaperMediaRepository
{
    void Insert(NonPaperMedia nonPaperMedia);
    Task<List<NonPaperMedia>> GetAllAsync();
    Task<NonPaperMedia?> GetByIdAsync(NpmId id);
    void Delete(NonPaperMedia nonPaper);
}
