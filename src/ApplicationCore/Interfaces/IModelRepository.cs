using Domain.Models.NPMs;

namespace ApplicationCore.Interfaces;

public interface IModelRepository
{
    Task<Model> GetOrCreateByName(string name, CancellationToken cancellationToken = default);
    Task<Model?> FindById(ModelId id, CancellationToken cancellationToken = default);
    Task<Model?> FindByName(string name, CancellationToken cancellationToken = default);
}
