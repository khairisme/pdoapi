using HR.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HR.Infrastructure.Data.EntityFramework
{
    public class PDODbContext : DbContext
    {
        public PDODbContext(DbContextOptions<PDODbContext> options) : base(options)
        {
        }
        public DbSet<PDOKumpulanPerkhidmatan> PDOKumpulanPerkhidmatan { get; set; }
        public DbSet<PDORujStatusPermohonan> PDORujStatusPermohonan { get; set; }
        public DbSet<PDOStatusPermohonanKumpulanPerkhidmatan> PDOStatusPermohonanKumpulanPerkhidmatan { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PDOKumpulanPerkhidmatan>().ToTable("PDO_KumpulanPerkhidmatan");

            modelBuilder.Entity<PDORujStatusPermohonan>(entity =>
            {
                entity.ToTable("PDO_RujStatusPermohonan"); 
                entity.HasKey(e => e.Kod); // Set Kod as the PK
                entity.Ignore(e => e.Id); // Don't map base Id
            });
            modelBuilder.Entity<PDOStatusPermohonanKumpulanPerkhidmatan>().ToTable("PDO_StatusPermohonanKumpulanPerkhidmatan");
            base.OnModelCreating(modelBuilder);
        }
    }
}
