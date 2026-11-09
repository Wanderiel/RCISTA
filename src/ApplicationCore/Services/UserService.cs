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

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _repository.GetAllAsync(cancellationToken);

    public async Task<User?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        UserId userId = new UserId(id);

        return await _repository.GetByIdAsync(userId, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        UserId userId = new UserId(id);
        User? user = await _repository.GetByIdAsync(userId, cancellationToken);

        if (user == null)
            return false;

        user.Block();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
