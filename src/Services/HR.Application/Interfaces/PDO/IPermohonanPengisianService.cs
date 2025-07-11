using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IPermohonanPengisianService
    {
        Task<List<PermohonanPOAFilterResponseDto>> GetPermohonanListPOAAsync(PermohonanPengisianPOAFilterDto search);
        Task<string> CreateAsync(SavePermohonanPengisianPOARequestDto request);
        Task<PermohonanPengisianHeaderResponseDto> GetPermohonanPengisianByAgensiAndId(PermohonanPengisianfilterdto filter);
        Task<bool> CheckDuplicateKodNamaAsync(SavePermohonanPengisianPOARequestDto dto);

         Task<List<BilanganPermohonanPengisianResponseDto>> GetBilanganPermohonanPengisianId(int idPermohonanPengisian);
        Task<bool> UpdateAsync(SavePermohonanPengisianPOARequestDto request);

        Task<List<PermohonanPOAIFilterResponseDto>> GetPermohonanListPOAIAsync(PermohonanPengisianPOAIFilterDto search);
        Task<List<PermohonanPengisianJawatanResponseDto>> GetFilteredPermohonanJawatanAsync(PermohonanPengisianJawatanFilterDto filter);
        Task<SkimNameWithJawatanDto?> GetJawatanBySkimAndAgensiAsync(PenolongPegawaiTeknologiMaklumatFilterDto filterDto);
        Task<List<AgensiWithJawatanDto>> GetGroupedJawatanByAgensiAsync(PenolongPegawaiTeknologiMaklumatFilterDto filter);
        Task<List<SimulasiKewanganByPermohonanDto>> GetSimulasiByPermohonanIdAsync(int idPermohonanPengisian);
        //Task<List<SimulasiKewanganResponseDto>> GetSimulasiByAgensiAsync(int agensiId);
        //Amar
        Task<List<SenaraiJawatanSebenarResponseDto>> GetSenaraiJawatanSebenar(SenaraiJawatanSebenarFilterDto filter);
        //Amar
        Task<List<ImplikasiKewanganResponseDto>> GetImplikasiKewangan(ImplikasiKewanganFilterDto filter);
        //Amar
        Task<List<SenaraiPermohonanPengisianJawatanResponseDto>> GetSenaraiPermohonanPengisianJawatan(SenaraiPermohonanPengisianJawatanFilterDto filter);
        //Amar
        Task<List<BilanganPermohonanPengisianMaklumatPermohonanResponseDto>> GetBilanganPermohonanPengisian(BilanganPermohonanPengisianFilterDto filter);
        //Amar
        Task<bool> SetHantarBilanganPermohonanPengisian(HantarBilanganPermohonanPengisianRequestDto request);
        //Amar
        Task<List<SenaraiJawatanSebenarGroupedAgencyResponseDto>> GetSenaraiJawatanSebenarGroupedAgency();
        //Amar
        Task<List<ImplikasiKewanganJanaSimulasiKewanganResponseDto>> GetImplikasiKewanganJanaSimulasiKewangan(ImplikasiKewanganJanaSimulasiKewanganFilterDto filter);

        //Nitya Code Start
        Task<PermohonanPengisianDto> getMaklumatPermohonanByIdAsync(int idPermohonanPengisian);

        Task<List<PermohonanSkimDetailDto>> GetSkimDetailsByAgensiIdAndNoRujukanAsync(int agensiId, string nomborRujukan);
        Task<MaklumatPermohonanDataDto> GetMaklumatPermohananData(int idPermohonanPengisian);
        Task<List<BilanganPermohonanPengisianDto>> GetBilanganPermohonanPengisian(int agensiId, string noRujukan);
        Task<bool> SimpanPermohonanDanSkimAsync(PermohonanUpdateDto request);
        Task<List<JawatanKekosonganDto>> GetJawatanKekosonganAsync(JawatanKekosonganFilterDto filter);
        Task<List<MaklumatPermohonanDto>> GetMaklumatPermohonanAsync(MaklumatPermohonanFilterDto filter);

        //Nitya Code End
    }
}
