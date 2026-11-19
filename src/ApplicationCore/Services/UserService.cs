using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Domain.Models.Users;

namespace ApplicationCore.Services;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<User>> GetAll(CancellationToken cancellationToken = default) =>
        await _repository.GetAll(cancellationToken);

    public async Task<User> Get(int id, CancellationToken cancellationToken = default) =>
        await GetUser(id, cancellationToken);

    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        User user = await GetUser(id, cancellationToken);
        user.Block();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task<User> GetUser(int id, CancellationToken cancellationToken)
    {
        UserId userId = new UserId(id);
        User user = await _repository.GetById(userId, cancellationToken) ??
            throw new MissingUserException(id);

        return user;
    }
}
