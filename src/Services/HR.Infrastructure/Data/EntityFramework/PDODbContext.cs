using HR.Core.Entities.PDO;
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
        public DbSet<PDOStatusPermohonanKlasifikasiPerkhidmatan> PDOStatusPermohonanKlasifikasiPerkhidmatan { get; set; }
        public DbSet<PDOKlasifikasiPerkhidmatan> PDOKlasifikasiPerkhidmatan { get; set; }

        public DbSet<PDOSkimPerkhidmatan> PDOSkimPerkhidmatan { get; set; }
        public DbSet<PDOGredSkimPerkhidmatan> PDOGredSkimPerkhidmatan { get; set; }
        public DbSet<PDOStatusPermohonanSkimPerkhidmatan> PDOStatusPermohonanSkimPerkhidmatan { get; set; }
        public DbSet<PDORujJenisSaraan> PDORujJenisSaraan { get; set; }


        public DbSet<PDOGred> PDOGred { get; set; }
        public DbSet<PDOStatusPermohonanGred> PDOStatusPermohonanGred { get; set; }

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
            modelBuilder.Entity<PDOStatusPermohonanKlasifikasiPerkhidmatan>().ToTable("PDO_StatusPermohonanKlasifikasiPerkhidmatan");
            modelBuilder.Entity<PDOKlasifikasiPerkhidmatan>().ToTable("PDO_KlasifikasiPerkhidmatan");

            modelBuilder.Entity<PDOSkimPerkhidmatan>().ToTable("PDO_SkimPerkhidmatan");
            //modelBuilder.Entity<PDOGredSkimPerkhidmatan>().ToTable("PDO_GredSkimPerkhidmatan");
            modelBuilder.Entity<PDOGredSkimPerkhidmatan>(entity =>
            {
                entity.ToTable("PDO_GredSkimPerkhidmatan");
                entity.HasKey(e => e.IdGred); // Set Kod as the PK
                entity.HasKey(e => e.IdSkimPerkhidmatan);

            });
            modelBuilder.Entity<PDORujJenisSaraan>(entity =>
            {
                entity.ToTable("PDO_RujJenisSaraan");
                entity.HasKey(e => e.Kod); // Set Kod as the PK
                entity.Ignore(e => e.Id); // Don't map base Id
            });
            modelBuilder.Entity<PDOStatusPermohonanSkimPerkhidmatan>().ToTable("PDO_StatusPermohonanSkimPerkhidmatan");


            modelBuilder.Entity<PDOGred>().ToTable("PDO_Gred");
            modelBuilder.Entity<PDOStatusPermohonanGred>().ToTable("PDO_StatusPermohonanGred");

            base.OnModelCreating(modelBuilder);
        }
    }
}
