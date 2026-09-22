using ApplicationCore.Interfaces;
using Domain.Models.Users;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SQLiteContext _context;

    public UserRepository(SQLiteContext context) =>
        _context = context;

    public void Insert(User user) =>
        _context.Users.Add(user);

    public async Task<List<User>> GetAllAsync() =>
        await _context.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(UserId id) =>
        await _context.Users.FindAsync(id);

    public async Task<User?> GetByLoginAsync(string login) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

    public async Task<bool> HasUserByLoginAsync(string login) =>
        await _context.Users.AnyAsync(u => u.Login == login);

    public void Delete(User user) =>
        _context.Users.Remove(user);
}
