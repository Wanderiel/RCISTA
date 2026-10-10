using Domain.Models.NPMs;

namespace ApplicationCore.Interfaces;

public interface IModelRepository
{
    Task<Model> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Model?> FindByIdAsync(ModelId id, CancellationToken cancellationToken = default);
    Task<Model?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}
