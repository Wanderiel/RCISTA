using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;

public class UserLookupService : IUserLookup
{
    private readonly IUserRepository _repository;

    public UserLookupService(IUserRepository repository) =>
        _repository = repository;

    public async Task<bool> HasUserByLogin(string username, CancellationToken cancellationToken = default) =>
        await _repository.HasUserByLogin(username, cancellationToken);
}
