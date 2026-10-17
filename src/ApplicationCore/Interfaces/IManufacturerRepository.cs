using Domain.Models.Base;

namespace ApplicationCore.Interfaces;

public interface IManufacturerRepository
{
    Task<Manufacturer> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Manufacturer?> FindByIdAsync(ManufacturerId id, CancellationToken cancellationToken = default);
    Task<Manufacturer?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}
