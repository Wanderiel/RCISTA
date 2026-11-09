using ApplicationCore.Interfaces;
using Domain.Models.Users;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostgresContext _context;

    public UserRepository(PostgresContext context) =>
        _context = context;

    public void Insert(User user) =>
        _context.Users.Add(user);

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Users.ToListAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default) =>
        await _context.Users.FindAsync(id, cancellationToken);

    public async Task<User?> GetByLoginAsync(string login, CancellationToken cancellationToken = default) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);

    public async Task<bool> HasUserByLoginAsync(string login, CancellationToken cancellationToken = default) =>
        await _context.Users.AnyAsync(u => u.Login == login, cancellationToken);
}
