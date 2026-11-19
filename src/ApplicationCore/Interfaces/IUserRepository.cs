using Domain.Models.Users;

namespace ApplicationCore.Interfaces;

public interface IUserRepository
{
    void Insert(User user);
    Task<List<User>> GetAll(CancellationToken cancellationToken = default);
    Task<User?> GetById(UserId id, CancellationToken cancellationToken = default);
    Task<User?> GetByLogin(string login, CancellationToken cancellationToken = default);
    Task<bool> HasUserByLogin(string login, CancellationToken cancellationToken = default);
}
