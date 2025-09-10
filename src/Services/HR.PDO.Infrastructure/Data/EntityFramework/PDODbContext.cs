using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Entities.PDP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System.Data.SqlTypes;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.SqlTypes;
using HR.PDO.Core.Entities;


namespace HR.PDO.Infrastructure.Data.EntityFramework
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
        public DbSet<PDORujStatusSkim> PDORujStatusSkim { get; set; }
        public DbSet<PDOSkimKetuaPerkhidmatan> PDOSkimKetuaPerkhidmatan { get; set; }
        public DbSet<PDOKetuaPerkhidmatan> PDOKetuaPerkhidmatan { get; set; }
        public DbSet<PDOJawatan> PDOJawatan { get; set; }
        public DbSet<PDOUnitOrganisasi> PDOUnitOrganisasi { get; set; }
        public DbSet<PDOPermohonanJawatan> PDOPermohonanJawatan { get; set; }
        public DbSet<PDOStatusPermohonanJawatan> PDOStatusPermohonanJawatan { get; set; }
        public DbSet<PDOAktivitiOrganisasi> PDOAktivitiOrganisasi { get; set; }
        public DbSet<PDORujKategoriAktivitiOrganisasi> PDORujKategoriAktivitiOrganisasi { get; set; }
        public DbSet<PDORujJenisPermohonan> PDORujJenisPermohonan { get; set; }
        public DbSet<PDOPermohonanPengisian> PDOPermohonanPengisian { get; set; }
        public DbSet<PDOStatusPermohonanPengisian> PDOStatusPermohonanPengisian { get; set; }
        public DbSet<PDORujJenisAgensi> PDORujJenisAgensi { get; set; }
        public DbSet<PDORujKategoriUnitOrganisasi> PDORujKategoriUnitOrganisasi { get; set; }
        public DbSet<PDOPengisianJawatan> PDOPengisianJawatan { get; set; }
        public DbSet<PDOGredSkimJawatan> PDOGredSkimJawatan { get; set; }
        public DbSet<PDOPermohonanPengisianSkim> PDOPermohonanPengisianSkim { get; set; }
        public DbSet<PDOStatusSkimPerkhidmatan> PDOStatusSkimPerkhidmatan { get; set; }
        public DbSet<PDORujStatusRekod> PDORujStatusRekod { get; set; }
        public DbSet<PDOKekosonganJawatan> PDOKekosonganJawatan { get; set; }
        public DbSet<PDORujStatusKekosonganJawatan> PDORujStatusKekosonganJawatan { get; set; }
        public DbSet<PDOButiranPermohonan> PDOButiranPermohonan { get; set; }
        public DbSet<PDORujStatusPermohonanJawatan> PDORujStatusPermohonanJawatan { get; set; }
        public DbSet<PDORujPasukanPerunding> PDORujPasukanPerunding { get; set; }
        public DbSet<PDOButiranPermohonanJawatan> PDOButiranPermohonanJawatan { get; set; }
        public DbSet<PDOButiranPermohonanSkimGred> PDOButiranPermohonanSkimGred { get; set; }
        public DbSet<PDOPenetapanImplikasiKewangan> PDOPenetapanImplikasiKewangan { get; set; }
        public DbSet<PDOButiranPermohonanSkimGredTBK> PDOButiranPermohonanSkimGredTBK { get; set; }
        public DbSet<PDODokumenPermohonan> PDODokumenPermohonan { get; set; }
        public DbSet<PDORujJenisDokumen> PDORujJenisDokumen { get; set; }
        public DbSet<PDOButiranPermohonanSkimGredKUJ> PDOButiranPermohonanSkimGredKUJ { get; set; }
        public DbSet<PDPJadualGaji> PDPJadualGaji { get; set; }
        public DbSet<PPARujPangkatBadanBeruniform> PPARujPangkatBadanBeruniform { get; set; }
        public DbSet<PDORujGelaranJawatan> PDORujGelaranJawatan { get; set; }
        public DbSet<PDORujKluster> PDORujKluster { get; set; }
        public DbSet<PDORujUrusanPerkhidmatan> PDORujUrusanPerkhidmatan { get; set; }
        public DbSet<PDORujJenisMesyuarat> PDORujJenisMesyuarat { get; set; }
        public DbSet<PDORujJenisJawatan> PDORujJenisJawatan { get; set; }
        public DbSet<PDORujStatusJawatan> PDORujStatusJawatan { get; set; }
        public DbSet<PDOCadanganJawatan> PDOCadanganJawatan { get; set; }
        public DbSet<PDORujKategoriJawatan> PDORujKategoriJawatan { get; set; }
        public DbSet<ProfilPemilikKompetensi> PPAProfilPemilikKompetensi { get; set; }
        public DbSet<PDOImplikasiPermohonanJawatan> PDOImplikasiPermohonanJawatan { get; set; }
        public DbSet<ONBSandangan> ONBSandangan { get; set; }
        






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProfilPemilikKompetensi>(entity =>
            {
                entity.ToTable("PPA_ProfilPemilikKompetensi");
                entity.HasNoKey();
            });
            modelBuilder.Entity<PDOButiranPermohonanJawatan>(entity =>
            {
                entity.ToTable("PDO_ButiranPermohonanJawatan");
                entity.HasKey(b => new { b.IdButiranPermohonan, b.IdJawatan });
                //entity.Ignore(e => e.Id); // Don't map base Id
            });
            modelBuilder.Entity<PDORujJenisDokumen>(entity =>
            {
                entity.ToTable("PDO_RujJenisDokumen"); 
                entity.HasKey(e => e.Kod); // Set Kod as the PK
                entity.Ignore(e => e.Id); // Don't map base Id
            });
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
            modelBuilder.Entity<PDODokumenPermohonan>(entity =>
            {
                entity.ToTable("PDO_DokumenPermohonan");
                entity.Ignore(e => e.StatusAktif); // Don't map base Id
            });
            modelBuilder.Entity<PDOButiranPermohonanSkimGred>(entity =>
            {
                entity.ToTable("PDO_ButiranPermohonanSkimGred");
                entity.Ignore(e => e.Id); // Don't map base Id
                entity.Ignore(e => e.StatusAktif); // Don't map base Id
                entity.HasNoKey();

                entity.HasKey(x => new { x.IdButiranPermohonan, x.IdSkimPerkhidmatan, x.IdGred });

            });
            modelBuilder.Entity<PDOButiranPermohonanSkimGredKUJ>(entity =>
            {
                entity.ToTable("PDO_ButiranPermohonanSkimGredKUJ");
                entity.Ignore(e => e.Id); // Don't map base Id
                entity.Ignore(e => e.StatusAktif); // Don't map base Id
                entity.HasNoKey();

                entity.HasKey(x => new { x.IdButiranPermohonan, x.IdSkim, x.IdGred });

            });
            modelBuilder.Entity<PDOPenetapanImplikasiKewangan>(e =>
            {
                e.ToTable("PDO_PenetapanImplikasiKewangan");
                e.HasKey(x => new { x.IdGred, x.IdSkimPerkhidmatan });
                e.Ignore(x => x.Id); // if inherited from BaseEntity
            });

            modelBuilder.Entity<PDOButiranPermohonanSkimGredTBK>(entity =>
            {
                entity.ToTable("PDO_ButiranPermohonanSkimGredTBK");
                entity.Ignore(e => e.Id); // Don't map base Id
                entity.Ignore(e => e.StatusAktif); // Don't map base Id
                entity.HasNoKey();

                entity.HasKey(x => new { x.IdButiranPermohonan, x.IdSkimPerkhidmatan, x.IdGred });

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
                entity.Ignore(e => e.StatusAktif);
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
                entity.Property(x => x.IdButiranPermohonanLama).IsRequired(false);
                entity.Property(x => x.IdSkimPerkhidmatanPemilikKompetensi).IsRequired(false);
                entity.Property(x => x.IdGredPemilikKompetensi).IsRequired(false);

                entity.Ignore(e => e.StatusAktif); // Don't map base Id
                entity.Ignore(nameof(PDOBaseEntity.StatusAktif)); // Don't map base Id

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
    

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
            {
                return SaveChangesInternal(() => base.SaveChanges(acceptAllChangesOnSuccess));
            }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default)
        {
            return SaveChangesInternal(() => base.SaveChangesAsync(acceptAllChangesOnSuccess, ct));
        }

        private async Task<int> SaveChangesInternal(Func<Task<int>> save)
        {
            try
            {
                return await save();
            }
            catch (DbUpdateException ex)
            {
                DumpBadDateTimes(); // tell us which fields are invalid
                throw;              // keep original stack/error
            }
        }

        private int SaveChangesInternal(Func<int> save)
        {
            try
            {
                return save();
            }
            catch (DbUpdateException)
            {
                DumpBadDateTimes();
                throw;
            }
        }

        private void DumpBadDateTimes()
        {
            var minSql = (DateTime)SqlDateTime.MinValue; // 1753-01-01
            foreach (var e in ChangeTracker.Entries()
                         .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                // Resolve table metadata to get actual column names
                var storeObj = StoreObjectIdentifier.Create(e.Metadata, StoreObjectType.Table);

                foreach (var p in e.Properties)
                {
                    var clr = p.Metadata.ClrType;
                    if (clr != typeof(DateTime) && clr != typeof(DateTime?)) continue;

                    DateTime? val = p.CurrentValue as DateTime?;
                    if (val.HasValue && val.Value < minSql)
                    {
                        var columnName = storeObj.HasValue
                            ? p.Metadata.GetColumnName(storeObj.Value)
                            : p.Metadata.GetColumnName(); // fallback

                        var colType = p.Metadata.GetColumnType() ?? "(unmapped)";
                        Console.WriteLine($"[BAD DATE] {e.Entity.GetType().Name}.{p.Metadata.Name} " +
                                          $"Column='{columnName}', SqlType='{colType}', Value='{val:O}'");
                    }
                }
            }
        }

}
}
