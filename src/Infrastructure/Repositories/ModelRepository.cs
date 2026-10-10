using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly SQLiteContext _context;

    public ModelRepository(SQLiteContext context) => _context = context;

    public async Task<Model> GetOrCreateByNameAsync(string name, CancellationToken cancellationTokenct = default)
    {
        Model? model = await FindByNameAsync(name, cancellationTokenct);

        if (model == null)
        {
            model = new Model(name);
            _context.Models.Add(model);
        }

        return model;
    }

    public async Task<Model?> FindByIdAsync(ModelId id, CancellationToken cancellationTokenct = default)
        => await _context.Models
            .FirstOrDefaultAsync(m => m.Id.Value == id.Value, cancellationTokenct);

    public async Task<Model?> FindByNameAsync(string name, CancellationToken cancellationTokenct = default)
        => await _context.Models
            .FirstOrDefaultAsync(m => m.Name == name, cancellationTokenct);
}
