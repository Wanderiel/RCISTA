using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;

public class NpmLookupService : INpmLookup
{
    private readonly INonPaperMediaRepository _repository;

    public NpmLookupService(INonPaperMediaRepository repository) =>
        _repository = repository;

    public async Task<bool> HasNpmBySerialnumber(string serialnumber, CancellationToken cancellationToken = default) =>
        await _repository.HasNpmBySerialnumber(serialnumber, cancellationToken);
}
