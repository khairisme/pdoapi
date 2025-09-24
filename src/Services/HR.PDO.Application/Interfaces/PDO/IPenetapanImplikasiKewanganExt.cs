using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IPenetapanImplikasiKewanganExt
    {
        public Task<ImplikasiKewanganDto> KosImplikasiKewangan(ImplikasiKewanganRequestDto request);
        public Task<SenaraiImplikasiKewanganOutputDto> SenaraiImplikasiKewangan(SenaraiImplikasiKewanganRequestDto request);
    }
}
