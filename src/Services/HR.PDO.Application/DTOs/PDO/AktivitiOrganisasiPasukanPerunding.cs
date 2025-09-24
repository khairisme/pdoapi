using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    public class AktivitiOrganisasiPasukanPerundingOutputDto
    {
        public string? NamaOrganisasi { get; set; }
        public string? PasukanPerunding { get; set; }

    }
    public class TambahAktivitiOrganisasiPasukanPerundingRequestDto
    {
        public string? KodRujPasukanPerunding { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }
    }

}
