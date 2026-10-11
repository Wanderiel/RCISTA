using ApplicationCore.Interfaces;
using Domain.Models.Base;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly SQLiteContext _context;

    public ManufacturerRepository(SQLiteContext context) =>
        _context = context;

    public async Task<Manufacturer> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        Manufacturer? manufacturer = await FindByNameAsync(name, cancellationToken);

        if (manufacturer == null)
        {
            manufacturer = new Manufacturer(name);
            _context.Manufacturers.Add(manufacturer);
        }

        return manufacturer;
    }

    public async Task<Manufacturer?> FindByIdAsync(ManufacturerId id, CancellationToken cancellationToken = default) =>
        await _context.Manufacturers.FindAsync(id, cancellationToken);

    public async Task<Manufacturer?> FindByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await _context.Manufacturers.FirstOrDefaultAsync(m => m.Name == name, cancellationToken);
}
