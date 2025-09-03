using HR.PDO.Application.Interfaces.PPA;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PPA
{
    public class ProfilPemilikKompetensiExtService : IProfilPemilikKompetensiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<ProfilPemilikKompetensiExtService> _logger;

        public ProfilPemilikKompetensiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ProfilPemilikKompetensiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<ProfilPemilikKompetensiDisplayDto>> CarianProfilPemilikKompetensi(ProfilPemilikKompetensiCarianDto filter)
        {
            try

            {
                var ProfilPemilikKompetensi = new List<ProfilPemilikKompetensiDto>
                {
                    new() { Bil = 1, NomborKadPengenalan="000000000001",NamaPemilikKompetensi="PEMILIK KOMPETENSI 1",NamaSkimPerkhidmatan="Pembantu Pembangunan Masyarakat",Gred="S19"},
                    new() { Bil = 2, NomborKadPengenalan="000000000002",NamaPemilikKompetensi="PEMILIK KOMPETENSI 2",NamaSkimPerkhidmatan="Pembantu Pendaftaran",Gred="KP19      "},
                    new() { Bil = 3, NomborKadPengenalan="000000000003",NamaPemilikKompetensi="PEMILIK KOMPETENSI 3",NamaSkimPerkhidmatan="Jururawat Masyarakat",Gred="U19       "},
                    new() { Bil = 4, NomborKadPengenalan="000000000004",NamaPemilikKompetensi="PEMILIK KOMPETENSI 4",NamaSkimPerkhidmatan="Pegawai Perkhidmatan Pendidikan",Gred="DG48      "},
                    new() { Bil = 5, NomborKadPengenalan="000000000005",NamaPemilikKompetensi="PEMILIK KOMPETENSI 5",NamaSkimPerkhidmatan="Pembantu Awam",Gred="H11"},
                    new() { Bil = 6, NomborKadPengenalan="000000000006",NamaPemilikKompetensi="PEMILIK KOMPETENSI 6",NamaSkimPerkhidmatan="Pembantu Ehwal Ekonomi",Gred="E22       "},
                    new() { Bil = 7, NomborKadPengenalan="000000000007",NamaPemilikKompetensi="PEMILIK KOMPETENSI 7",NamaSkimPerkhidmatan="Pembantu Tadbir (Perkeranian/Operasi)",Gred="N22       "},
                    new() { Bil = 8, NomborKadPengenalan="000000000008",NamaPemilikKompetensi="PEMILIK KOMPETENSI 8",NamaSkimPerkhidmatan="Pegawai Perkhidmatan Pendidikan",Gred="DG54      "},
                    new() { Bil = 9, NomborKadPengenalan="000000000009",NamaPemilikKompetensi="PEMILIK KOMPETENSI 9",NamaSkimPerkhidmatan="Pegawai Perkhidmatan Pendidikan",Gred="DG44      "},
                    new() { Bil = 10, NomborKadPengenalan="000000000010",NamaPemilikKompetensi="PEMILIK KOMPETENSI 10",NamaSkimPerkhidmatan="Pegawai Tadbir",Gred="N48       "}
                };

                var result = (from pdoppk in ProfilPemilikKompetensi
                             where (filter.NomborKadPengenalan!=null && pdoppk.NomborKadPengenalan.Contains(filter.NomborKadPengenalan) || filter.NomborKadPengenalan == null)
                             && (filter.NamaPemilikKompetensi!=null && pdoppk.NamaPemilikKompetensi.Contains(filter.NamaPemilikKompetensi) || filter.NamaPemilikKompetensi==null)
                             select new ProfilPemilikKompetensiDisplayDto
                             {
                                 Bil= pdoppk.Bil,
                                 NomborKadPengenalan=pdoppk.NomborKadPengenalan,
                                 NamaPemilikKompetensi = pdoppk.NamaPemilikKompetensi,
                                 NamaSkimPerkhidmatan = pdoppk.NamaSkimPerkhidmatan,
                                 Gred = pdoppk.Gred
                             }).ToList();


                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in CarianSenaraiPermohonanJawatan");

                throw;
            }

        }



    }

}

