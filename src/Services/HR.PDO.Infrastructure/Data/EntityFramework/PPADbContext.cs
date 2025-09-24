using HR.PDO.Core.Entities.PDO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using HR.PDO.Core.Entities.PPA;
namespace HR.PDO.Infrastructure.Data.EntityFramework
{
    public class PPADbContext : DbContext
    {
        public PPADbContext(DbContextOptions<PDODbContext> options) : base(options)
        {
        }
        public DbSet<PPARujPangkatBadanBeruniform> PPARujPangkatBadanBeruniform { get; set; }



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
                entity.HasKey(e => new { e.IdGred, e.IdSkimPerkhidmatan });

            });
            modelBuilder.Entity<PDORujJenisSaraan>(entity =>
            {
                entity.ToTable("PDO_RujJenisSaraan");
                entity.HasKey(e => e.Kod); // Set Kod as the PK
                entity.Ignore(e => e.Id); // Don't map base Id
            });
            modelBuilder.Entity<PDORujStatusSkim>(entity =>
            {
                entity.ToTable("PDO_RujStatusSkim");
                entity.HasKey(e => e.Kod);
                entity.Ignore(e => e.Id); 
            });
            modelBuilder.Entity<PDORujJenisPermohonan>(entity =>
            {
                entity.ToTable("PDO_RujJenisPermohonan");

                entity.HasKey(e => e.Kod); // Don't map base Id
            });
            modelBuilder.Entity<PDOStatusPermohonanSkimPerkhidmatan>().ToTable("PDO_StatusPermohonanSkimPerkhidmatan");


            modelBuilder.Entity<PDOGred>().ToTable("PDO_Gred");
            modelBuilder.Entity<PDOStatusPermohonanGred>().ToTable("PDO_StatusPermohonanGred");

            modelBuilder.Entity<PDOSkimKetuaPerkhidmatan>().ToTable("PDO_SkimKetuaPerkhidmatan")
             .HasKey(p => new { p.IdSkimPerkhidmatan, p.IdKetuaPerkhidmatan });

            modelBuilder.Entity<PDOKetuaPerkhidmatan>().ToTable("PDO_KetuaPerkhidmatan");

            modelBuilder.Entity<PDOJawatan>().ToTable("PDO_Jawatan");

            modelBuilder.Entity<PDOUnitOrganisasi>().ToTable("PDO_UnitOrganisasi");

            
            modelBuilder.Entity<PDOPermohonanJawatan>(entity =>
            {
                entity.ToTable("PDO_PermohonanJawatan");
                entity.HasKey(e => e.Id); 
            });

            modelBuilder.Entity<PDOStatusPermohonanJawatan>().ToTable("PDO_StatusPermohonanJawatan");

            modelBuilder.Entity<PDOAktivitiOrganisasi>().ToTable("PDO_AktivitiOrganisasi");
            modelBuilder.Entity<PDORujKategoriAktivitiOrganisasi>(entity =>
            {
                entity.ToTable("PDO_RujKategoriAktivitiOrganisasi");
                entity.Ignore(e => e.Id);
                entity.HasKey(e => e.Kod);
            });

            modelBuilder.Entity<PDOPermohonanPengisian>(entity =>
            {
                entity.ToTable("PDO_PermohonanPengisian");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<PDOStatusPermohonanPengisian>(entity =>
            {
                entity.ToTable("PDO_StatusPermohonanPengisian");
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<PDORujJenisAgensi>(entity =>
            {
                entity.ToTable("PDO_RujJenisAgensi");
                entity.HasKey(e => e.Kod); 
                entity.Ignore(e => e.Id); 
            });

            modelBuilder.Entity<PDORujKategoriUnitOrganisasi>(entity =>
            {
                entity.ToTable("PDO_RujKategoriUnitOrganisasi");
                entity.HasKey(e => e.Kod);
                entity.Ignore(e => e.Id);
            });

            modelBuilder.Entity<PDOPengisianJawatan>(entity =>
            {
                entity.ToTable("PDO_PengisianJawatan");
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<PDOGredSkimJawatan>(entity =>
            {
                entity.ToTable("PDO_GredSkimJawatan");
                entity.HasKey(e => new { e.IdJawatan, e.IdGred, e.IdSkimPerkhidmatan });
            });

            modelBuilder.Entity<PDOPermohonanPengisianSkim>(entity =>
            {
                entity.ToTable("PDO_PermohonanPengisianSkim");
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<PDOStatusSkimPerkhidmatan>(entity =>
            {
                entity.ToTable("PDO_StatusSkimPerkhidmatan");
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<PDORujStatusRekod>(entity =>
            {
                entity.ToTable("PDO_RujStatusRekod");
                entity.HasKey(e => e.Kod);
                entity.Ignore(e => e.Id);
            });
            modelBuilder.Entity<PDOKekosonganJawatan>(entity =>
            {
                entity.ToTable("PDO_KekosonganJawatan");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<PDORujStatusKekosonganJawatan>(entity =>
            {
                entity.ToTable("PDO_RujStatusKekosonganJawatan");
                entity.HasKey(e => e.Kod);
                entity.Ignore(e => e.Id);
            });
            modelBuilder.Entity<PDOButiranPermohonan>(entity =>
            {
                entity.ToTable("PDO_ButiranPermohonan");
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<PDORujPasukanPerunding>(entity =>
            {
                entity.ToTable("PDO_RujPasukanPerunding");
                entity.HasKey(e => e.Kod);
                entity.Ignore(e => e.Id);
            });

          
            modelBuilder.Entity<PDORujStatusPermohonanJawatan>(entity =>
            {
                entity.ToTable("PDO_RujStatusPermohonanJawatan");
                entity.HasKey(e => e.Kod);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
