using Azure.Core;
using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities.PDO;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    /// <summary>
    /// Interface for Butiran Permohonan operations including creation, updates, transfers, and retrievals.
    /// </summary>
    public interface IButiranPermohonanExt
    {
        /// <summary>
        /// Adds a new Butiran Permohonan record.
        /// </summary>
        /// <param name="request">DTO containing the details to be added.</param>
        /// <returns>The created Butiran Permohonan entity.</returns>
        Task<PDOButiranPermohonan> TambahButiranPermohonan(TambahButiranPermohonanDto request);
        Task<string?> ButiranPermohonanTagJawatan(ButiranPermohonanTagJawatanRequestDto request);
        

        /// <summary>
        /// Updates an existing Butiran Permohonan record.
        /// </summary>
        /// <param name="request">DTO containing the updated details.</param>
        Task KemaskiniButiranPermohonan(KemaskiniButiranPermohonanRequestDto request);

        /// <summary>
        /// Deactivates a specific Butiran Jawatan record.
        /// </summary>
        /// <param name="request">DTO containing the identifier and deactivation details.</param>
        Task<PDOButiranPermohonan> MansuhButiranButiranJawatan(MansuhButiranJawatanRequestDto request);

        /// <summary>
        /// Updates specific change details (butir perubahan) in a Butiran Permohonan record.
        /// </summary>
        /// <param name="request">DTO containing the change details to be updated.</param>
        Task KemaskiniButiranPerubahanButiranPermohonan(KemaskiniButiranPermohonanRequestDto request);

        /// <summary>
        /// Calculates the financial implications of a Butiran Permohonan.
        /// </summary>
        /// <param name="request">DTO containing parameters for financial calculation.</param>
        Task KiraImplikasiKewanganButiranPermohonan(KiraImplikasiKewanganRequestDto request);

        /// <summary>
        /// Retrieves a specific Butiran Permohonan record by its unique identifier.
        /// </summary>
        /// <param name="IdPermohonanJawatan">The unique identifier of the record.</param>
        /// <returns>The matching Butiran Permohonan DTO.</returns>
        Task<TambahButiranPermohonanDto> BacaButiranPermohonan(int? IdPermohonanJawatan);

        Task<TambahButiranPermohonanDto> BacaRekodButiranPermohonan(int? Id);
        /// <summary>
        /// Retrieves a list of all Butiran Permohonan records.
        /// </summary>
        /// <returns>List of Butiran Permohonan DTOs.</returns>
        Task<List<TambahButiranPermohonanDto>> SenaraiButiranPermohonan();

        /// <summary>
        /// Loads the full Butiran Permohonan data set.
        /// </summary>
        /// <returns>Structured output DTO containing loaded data.</returns>
        Task<ButiranPermohonanLoadOutputDto> MuatButiranPermohonan(int? IdPermohonanJawatan, int? IdButiranPermohonan);

        /// <summary>
        /// Transfers Butiran Permohonan data to a new context or state.
        /// </summary>
        /// <param name="request">DTO containing transfer details.</param>
        Task PindahButiranPermohonan(PindahButiranPermohonanRequestDto request);

        /// <summary>
        /// Retrieves Butiran Permohonan records filtered by Aktiviti Organisasi.
        /// </summary>
        /// <param name="IdPermohonanJawatan">Identifier used for filtering.</param>
        /// <returns>List of filtered Butiran Permohonan DTOs.</returns>
        Task<List<ButiranPermohonanIkutAktivitiOrganisasiDto>> ButiranPermohonanIkutAktivitiOrganisasi(int IdPermohonanJawatan);
    }
}