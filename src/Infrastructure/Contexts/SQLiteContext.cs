using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class SQLiteContext : DbContext, IUnitOfWork
{
    public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) =>
        Database.EnsureCreated();

    public DbSet<NonPaperMedia> NonPaperMedias { get; set; }

    public override int SaveChanges()
    {
        SetTimestampsForUsers();

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestampsForUsers();

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NonPaperMedia>(builder =>
        {
            builder.Property(p => p.Id)
            .HasConversion(
                npmId => npmId.Value,
                npmId => new NpmId(npmId));
        });

        base.OnModelCreating(modelBuilder);
    }

    private void SetTimestampsForUsers()
    {
        var modifiedEntities = ChangeTracker.Entries<NonPaperMedia>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in modifiedEntities)
            entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
    }
}
