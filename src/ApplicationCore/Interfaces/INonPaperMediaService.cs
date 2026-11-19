using ApplicationCore.Dtos.NPMs;

namespace ApplicationCore.Interfaces;

public interface INonPaperMediaService
{
    Task Create(NpmRequest dto, CancellationToken cancellationToken = default);
    Task<List<NpmResponse>> GetAll(CancellationToken cancellationToken = default);
    Task<NpmResponse?> Get(int id, CancellationToken cancellationToken = default);
    Task Delete(int id, CancellationToken cancellationToken = default);
    Task Update(int id, UpdatedNPMDto npmDto, CancellationToken cancellationToken = default);
}