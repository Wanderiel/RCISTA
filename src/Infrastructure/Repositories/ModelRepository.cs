using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly PostgresContext _context;

    public ModelRepository(PostgresContext context) =>
        _context = context;

    public async Task<Model> GetOrCreateByName(string name, CancellationToken cancellationToken = default)
    {
        Model? model = await FindByName(name, cancellationToken);

        if (model == null)
        {
            model = new Model(name);
            _context.Models.Add(model);
        }

        return model;
    }

    public async Task<Model?> FindById(ModelId id, CancellationToken cancellationToken = default) =>
        await _context.Models.FindAsync(id, cancellationToken);

    public async Task<Model?> FindByName(string name, CancellationToken cancellationToken = default) =>
        await _context.Models.FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower(), cancellationToken);
}
