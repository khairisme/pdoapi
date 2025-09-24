using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using  Shared.Contracts.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/alamat-unit-organisasi")]
    // Author: Khairi
    // Date: 15 Sept 2025
    // Purpose: Controller to handle external operations related to Alamat Unit Organisasi, including adding new records.

    public class AlamatUnitOrganisasiExtController : ControllerBase
    {
        // Logger instance for tracking execution and errors
        private readonly ILogger<AlamatUnitOrganisasiExtController> _logger;

        // Service interface for AlamatUnitOrganisasi external operations
        private readonly IAlamatUnitOrganisasiExt _AlamatUnitOrganisasiext;

        // Author: Khairi
        // Date: 15 Sept 2025
        // Purpose: Constructor injection for service and logger dependencies
        public AlamatUnitOrganisasiExtController(IAlamatUnitOrganisasiExt AlamatUnitOrganisasiext, ILogger<AlamatUnitOrganisasiExtController> logger)
        {
            _AlamatUnitOrganisasiext = AlamatUnitOrganisasiext;
            _logger = logger;
        }

        // Author: Khairi
        // Date: 15 Sept 2025
        // Purpose: API endpoint to add a new Alamat Unit Organisasi record
        [HttpPost("tambah")]
        public async Task<ActionResult<object>> TambahAlamatUnitOrganisasi(AlamatUnitOrganisasiDto request)
        {
            _logger.LogInformation("Calling BacaUnitOrganisasi");

            try
            {
                var data = await _AlamatUnitOrganisasiext.TambahAlamatUnitOrganisasi(request);
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                string err = "";

                if (ex != null)
                {
                    _logger.LogError(ex, "Error in BacaUnitOrganisasi");

                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + " - " + err);
            }
        }
    }
                }
