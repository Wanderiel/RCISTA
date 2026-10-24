using ApplicationCore.Interfaces;
using Domain.Interfaces;
using Domain.Models.Base;
using Domain.Models.NPMs;
using Domain.Models.Users;
using Domain.Models.Workstations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class PostgresContext : DbContext, IUnitOfWork
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) =>
        Database.EnsureCreated();

    public DbSet<User> Users { get; set; }
    public DbSet<Workstation> Workstations { get; set; }
    public DbSet<NonPaperMedia> NonPaperMedias { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Model> Models { get; set; }

    public override int SaveChanges()
    {
        SetTimestamps();

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    userId => userId.Value,
                    id => new UserId(id))
                .ValueGeneratedOnAdd();

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
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    workstationId => workstationId.Value,
                    id => new WorkstationId(id))
                .ValueGeneratedOnAdd();

            builder.HasIndex(w => w.InventoryNumber)
                .IsUnique();

            builder.HasMany(w => w.Disks)
                .WithOne()
                .HasForeignKey("WorkstationId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<NonPaperMedia>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    npmId => npmId.Value,
                    id => new NpmId(id))
                .ValueGeneratedOnAdd();

            builder.HasOne(npm => npm.Manufacturer)
                .WithMany()
                .HasForeignKey("ManufacturerId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(npm => npm.Model)
                .WithMany()
                .HasForeignKey("ModelId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(npm => npm.SerialNumber)
                .IsUnique();
        });

        modelBuilder.Entity<Manufacturer>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    manufacturerId => manufacturerId.Value,
                    id => new ManufacturerId(id))
                .ValueGeneratedOnAdd();

            builder.HasIndex(manufacturer => manufacturer.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Model>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    modelId => modelId.Value,
                    id => new ModelId(id))
                .ValueGeneratedOnAdd();

            builder.HasIndex(model => model.Name)
                .IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }

    private void SetTimestamps()
    {
        SetTimestampForAdded();
        SetTimestampForMidified();
    }

    private void SetTimestampForAdded()
    {
        var modifiedEntities = ChangeTracker.Entries<IChangedAt>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in modifiedEntities)
            entry.Property(e => e.CreatedAt).CurrentValue = DateTime.UtcNow;
    }

    private void SetTimestampForMidified()
    {
        var modifiedEntities = ChangeTracker.Entries<IChangedAt>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in modifiedEntities)
            entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
    }
}
