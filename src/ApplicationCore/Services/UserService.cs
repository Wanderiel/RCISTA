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

    public async Task<List<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User?> GetAsync(int id)
    {
        UserId userId = new UserId(id);

        return await _repository.GetByIdAsync(userId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        UserId userId = new UserId(id);
        User? user = await _repository.GetByIdAsync(userId);

        if (user == null)
            return false;

        _repository.Delete(user);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
