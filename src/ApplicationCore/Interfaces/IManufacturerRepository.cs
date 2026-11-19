using Domain.Models.Base;

namespace ApplicationCore.Interfaces;

public interface IManufacturerRepository
{
    Task<Manufacturer> GetOrCreateByName(string name, CancellationToken cancellationToken = default);
    Task<Manufacturer?> FindById(ManufacturerId id, CancellationToken cancellationToken = default);
    Task<Manufacturer?> FindByName(string name, CancellationToken cancellationToken = default);
}
