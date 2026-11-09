using Domain.Models.Users;

namespace ApplicationCore.Interfaces;

public interface IUserRepository
{
    void Insert(User user);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<User?> GetByLoginAsync(string login, CancellationToken cancellationToken = default);
    Task<bool> HasUserByLoginAsync(string login, CancellationToken cancellationToken = default);
}
