using HR.Core.Entities.PDO;
using HR.Core.Entities.PDP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HR.Infrastructure.Data.EntityFramework
{
    public class PDPDbContext : DbContext
    {
        public PDPDbContext(DbContextOptions<PDPDbContext> options) : base(options)
        {
        }
        public DbSet<PDPJadualGaji> PDPJadualGaji { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PDPJadualGaji>(entity =>
            {
                entity.ToTable("PDP_JadualGaji");
                entity.HasKey(e => e.Id);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
