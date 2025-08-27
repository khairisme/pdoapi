using HR.PDO.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HR.PDO.Infrastructure.Data.EntityFramework;

/// <summary>
/// Entity Framework DbContext for the HR application
/// </summary>
public class HRDbContext : DbContext
{
    public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
    {
    }

    // DbSet properties for each entity
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<Attendance> Attendances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entity relationships and constraints
        modelBuilder.Entity<Department>(entity =>
        { //test
            entity.ToTable("Department");
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Description).HasMaxLength(500);
            entity.Property(d => d.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.IsDeleted).HasDefaultValue(false);

            entity.HasOne<Department>()
                .WithMany()
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.ToTable("Leave");
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Reason).HasMaxLength(500);
            entity.Property(l => l.IsDeleted).HasDefaultValue(false);

        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.ToTable("Attendance");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.IsDeleted).HasDefaultValue(false);

        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Set audit properties before saving changes - Text
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    if (string.IsNullOrEmpty(entry.Entity.CreatedBy))
                        entry.Entity.CreatedBy = "System";
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    if (string.IsNullOrEmpty(entry.Entity.ModifiedBy))
                        entry.Entity.ModifiedBy = "System";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
