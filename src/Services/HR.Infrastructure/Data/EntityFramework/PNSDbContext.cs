using HR.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HR.Infrastructure.Data.EntityFramework
{
    public class PNSDbContext : DbContext
    {
        public PNSDbContext(DbContextOptions<PNSDbContext> options) : base(options)
        {
        }

        // Example DbSets
        public DbSet<Pengguna> Pengguna { get; set; }
        public DbSet<PNSSoalanKeselamatan> PNSSoalanKeselamatan { get; set; }
        public DbSet<PNSGambar> PNSGambar { get; set; }

        public DbSet<PerkhidmatanLog> PerkhidmatanLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pengguna>().ToTable("PNS_Pengguna");
            modelBuilder.Entity<PNSSoalanKeselamatan>().ToTable("PNS_SoalanKeselamatan");
            modelBuilder.Entity<PNSGambar>().ToTable("PNS_Gambar");
            modelBuilder.Entity<PerkhidmatanLog>().ToTable("PNS_PerkhidmatanLog");
            base.OnModelCreating(modelBuilder);
        }
    }
}
