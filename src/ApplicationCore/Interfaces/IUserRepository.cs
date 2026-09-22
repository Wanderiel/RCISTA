using Domain.Models.Users;

namespace ApplicationCore.Interfaces;

public interface IUserRepository
{
    void Insert(User user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByLoginAsync(string login);
    Task<bool> HasUserByLoginAsync(string login);
    void Delete(User user);
}
