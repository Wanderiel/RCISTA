using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class SQLiteContext : DbContext, IUnitOfWork
{
    public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
