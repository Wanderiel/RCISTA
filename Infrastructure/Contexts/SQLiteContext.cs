using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class SQLiteContext : DbContext, IUnitOfWork
{
    public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) =>
        Database.EnsureCreated();

    public DbSet<NonPaperMedia> NonPaperMedias { get; set; }
}
