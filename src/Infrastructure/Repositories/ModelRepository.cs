using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly SQLiteContext _context;

    public ModelRepository(SQLiteContext context) =>
        _context = context;

    public async Task<Model> GetOrCreateByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        Model? model = await FindByNameAsync(name, cancellationToken);

        if (model == null)
        {
            model = new Model(name);
            _context.Models.Add(model);
        }

        return model;
    }

    public async Task<Model?> FindByIdAsync(ModelId id, CancellationToken cancellationToken = default)
        => await _context.Models.FindAsync(id, cancellationToken);

    public async Task<Model?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        => await _context.Models.FirstOrDefaultAsync(m => m.Name == name, cancellationToken);
}
