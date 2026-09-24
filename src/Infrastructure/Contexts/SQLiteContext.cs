using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Domain.Models.Users;
using Domain.Models.Workstations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class SQLiteContext : DbContext, IUnitOfWork
{
    public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) =>
        Database.EnsureCreated();

    public DbSet<User> Users { get; set; }
    public DbSet<Workstation> Workstations { get; set; }
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
        modelBuilder.Entity<User>(builder =>
        {
            builder.Property(p => p.Id)
                .HasConversion(
                    userId => userId.Value,
                    userId => new UserId(userId));

            builder.OwnsOne(
                user => user.FullName,
                fullName =>
                {
                    fullName.Property(p => p.FirstName).HasColumnName("FirstName");
                    fullName.Property(p => p.LastName).HasColumnName("LastName");
                    fullName.Property(p => p.Patronymic).HasColumnName("Patronymic");
                });

            builder.HasIndex(u => u.Login)
                .IsUnique();
        });

        modelBuilder.Entity<Workstation>(builder =>
        {
            builder.Property(p => p.Id)
                .HasConversion(
                    workstationId => workstationId.Value,
                    workstationId => new WorkstationId(workstationId));

            builder.HasIndex(w => w.Inventory)
                .IsUnique();

            builder.HasMany(w => w.Disks)
                .WithOne()
                .HasForeignKey()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<NonPaperMedia>(builder =>
        {
            builder.Property(p => p.Id)
                .HasConversion(
                    npmId => npmId.Value,
                    npmId => new NpmId(npmId));

            builder.HasIndex(npm => npm.SerialNumber)
                .IsUnique();
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
