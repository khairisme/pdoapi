using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    /// <summary>
    /// Defines contract methods for managing Aktiviti Organisasi operations,
    /// including creation, update, retrieval, and deletion.
    /// </summary>
    /// 
    public interface IAktivitiOrganisasiExt
    {
        /// <summary>
        /// Retrieves a paged structure of Aktiviti Organisasi records based on filter criteria.
        /// </summary>
        /// <param name="request">The request containing pagination, filtering, and sorting parameters.</param>
        /// <returns>A paged result of StrukturAktivitiOrganisasiDto objects.</returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Provides paginated and filterable access to AktivitiOrganisasi records for listing and UI binding.  
        /// </remarks>
        public Task<PagedResult<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasi(StrukturAktivitiOrganisasiRequestDto request);

        public Task<PagedResult<StrukturAktivitiOrganisasiDto>> StrukturButiranAktivitiOrganisasi(StrukturAktivitiOrganisasiRequestDto request);
        
        /// <summary>
        /// Creates a new Aktiviti Organisasi record.
        /// </summary>
        /// <param name="request">The request payload containing details of the new Aktiviti Organisasi.</param>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Registers a new AktivitiOrganisasi entity with provided details.  
        /// </remarks>
        public Task<AktivitiOrganisasiDto> WujudAktivitiOrganisasiBaru(WujudAktivitiOrganisasiRequestDto request);

        /// <summary>
        /// Renames (rebrands) an existing Aktiviti Organisasi entity.
        /// </summary>
        /// <param name="request">The request payload containing the Id, UserId, and new name.</param>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Allows renaming of an existing AktivitiOrganisasi for rebranding or correction.  
        /// </remarks>
        public Task PenjenamaanAktivitiOrganisasi(PenjenamaanAktivitiOrganisasiRequestDto request);

        /// <summary>
        /// Moves an Aktiviti Organisasi to a different parent node.
        /// </summary>
        /// <param name="request">The request payload containing UserId, entity Id, and parent node changes.</param>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Supports restructuring of AktivitiOrganisasi hierarchy by moving an entity.  
        /// </remarks>
        public Task PindahAktivitiOrganisasi(PindahAktivitiOrganisasiRequestDto request);

        /// <summary>
        /// Retrieves a single Aktiviti Organisasi record by Id.
        /// </summary>
        /// <param name="Id">The identifier of the Aktiviti Organisasi.</param>
        /// <returns>An AktivitiOrganisasiDto object if found, otherwise null.</returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Fetches detailed information about an AktivitiOrganisasi, including hierarchy and codes.  
        /// </remarks>
        public Task<AktivitiOrganisasiDto> BacaAktivitiOrganisasi(int Id);

        public Task<AktivitiOrganisasiAlamatIndukDto> BacaAktivitiOrganisasiAlamatInduk(int IdUnitOrganisasi);
         /// <summary>
         /// Retrieves a list of Aktiviti Organisasi for use in dropdowns or reference data.
         /// </summary>
         /// <returns>A list of DropDownDto objects containing code and name pairs.</returns>
         /// <remarks>
         /// Author      : Khairi bin Abu Bakar  
         /// Created On  : 2025-09-03  
         /// Purpose     : Provides simple lookup values for binding AktivitiOrganisasi in dropdowns and selection lists.  
         /// </remarks>
        public Task<List<DropDownDto>> RujukanAktivitiOrganisasi();

        /// <summary>
        /// Registers a new Aktiviti Organisasi record.
        /// </summary>
        /// <param name="request">The request payload containing user and Aktiviti details.</param>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Handles insertion of a new AktivitiOrganisasi entity into the database.  
        /// </remarks>
        public Task DaftarAktivitiOrganisasi(AktivitiOrganisasiDaftarRequestDto request);

        /// <summary>
        /// Permanently deletes an Aktiviti Organisasi record.
        /// </summary>
        /// <param name="request">The request payload containing UserId and entity Id.</param>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Removes an AktivitiOrganisasi entity permanently from the system.  
        /// </remarks>
        public Task HapusTerusAktivitiOrganisasi(HapusTerusAktivitiOrganisasiRequestDto request);

        /// <summary>
        /// Deactivates (soft deletes / retires) an Aktiviti Organisasi entity.
        /// </summary>
        /// <param name="UserId">The user performing the operation.</param>
        /// <param name="Id">The identifier of the Aktiviti Organisasi to deactivate.</param>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Marks an AktivitiOrganisasi entity as inactive without permanently deleting it.  
        /// </remarks>
        public Task MansuhAktivitiOrganisasi(MansuhAktivitiOrganisasiRequestDto request);
    }
}
