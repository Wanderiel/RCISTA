using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;

public class UserLookupService : IUserLookup
{
    private readonly IUserRepository _repository;

    public UserLookupService(IUserRepository repository) =>
        _repository = repository;

    public async Task<bool> HasUserByLoginAsync(string username) =>
        await _repository.HasUserByLoginAsync(username);
}
