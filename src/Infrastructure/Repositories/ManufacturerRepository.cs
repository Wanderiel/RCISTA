using ApplicationCore.Interfaces;
using Domain.Models.Base;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly PostgresContext _context;

    public ManufacturerRepository(PostgresContext context) =>
        _context = context;

    public async Task<Manufacturer> GetOrCreateByName(string name, CancellationToken cancellationToken = default)
    {
        Manufacturer? manufacturer = await FindByName(name, cancellationToken);

        if (manufacturer == null)
        {
            manufacturer = new Manufacturer(name);
            _context.Manufacturers.Add(manufacturer);
        }

        return manufacturer;
    }

    public async Task<Manufacturer?> FindById(ManufacturerId id, CancellationToken cancellationToken = default) =>
        await _context.Manufacturers.FindAsync(id, cancellationToken);

    public async Task<Manufacturer?> FindByName(string name, CancellationToken cancellationToken = default) =>
        await _context.Manufacturers.FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower(), cancellationToken);
}
