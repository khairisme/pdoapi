using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan")]
    public class ButiranPermohonanExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanExtController> _logger;
        private readonly IButiranPermohonanExt _butiranpermohonanext;
        private readonly IRujStatusJawatanExt _rujstatusjawatanExt;

        public ButiranPermohonanExtController(IButiranPermohonanExt butiranpermohonanext, IRujStatusJawatanExt rujstatusjawatanExt, ILogger<ButiranPermohonanExtController> logger)
        {
            _butiranpermohonanext = butiranpermohonanext;
            _rujstatusjawatanExt = rujstatusjawatanExt;
            _logger = logger;
        }

        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP POST requests to add a new Butiran Permohonan record.
        //          Logs the operation, delegates to the service layer, and returns appropriate HTTP responses.

        /// <summary>
        /// Adds a new Butiran Permohonan record.
        /// </summary>
        /// <param name="request">The DTO containing details of the Butiran Permohonan to be added.</param>
        /// <returns>Returns HTTP 200 OK with the created record, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully added the Butiran Permohonan.</response>
        /// <response code="500">Internal server error occurred while processing the request.</response>
        [HttpPost("tambah")] // Defines the route as POST /tambah
        public async Task<ActionResult> TambahButiranPermohonan([FromBody] TambahButiranPermohonanDto request)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling TambahButiranPermohonan");

            try
            {
                // Call the service layer to process the incoming request and add the record
                var data = await _butiranpermohonanext.TambahButiranPermohonan(request);

                // Return HTTP 200 OK with the result data
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                // Initialize an empty string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in TambahButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }

        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP POST requests to update an existing Butiran Permohonan record.
        //          Logs the operation, delegates to the service layer, and returns appropriate HTTP responses.

        /// <summary>
        /// Updates an existing Butiran Permohonan record.
        /// </summary>
        /// <param name="request">The DTO containing updated details of the Butiran Permohonan.</param>
        /// <returns>Returns HTTP 200 OK with a success message, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully updated the Butiran Permohonan.</response>
        /// <response code="500">Internal server error occurred while processing the update.</response>
        [HttpPost("kemas-kini")] // Defines the route as POST /kemas-kini
        public async Task<ActionResult> KemaskiniButiranPermohonan([FromBody] KemaskiniButiranPermohonanRequestDto request)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling KemaskiniButiranPermohonan");

            try
            {
                // Call the service layer to process the update request
                await _butiranpermohonanext.KemaskiniButiranPermohonan(request);

                // Return HTTP 200 OK with a success message
                return Ok(new { message = "Berjaya Kemaskini Butiran Permohonan" });
            }
            catch (Exception ex)
            {
                // Initialize an empty string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in KemaskiniButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP POST requests to load or retrieve Butiran Permohonan data.
        //          Logs the operation, delegates to the service layer, and returns the result or error response.

        /// <summary>
        /// Retrieves the full list of Butiran Permohonan records.
        /// </summary>
        /// <returns>Returns HTTP 200 OK with the list of records, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully retrieved Butiran Permohonan data.</response>
        /// <response code="500">Internal server error occurred while retrieving data.</response>
        [HttpPost("muat")] // Defines the route as POST /muat
        public async Task<ActionResult> MuatButiranPermohonan(int? IdPermohonanJawatan, int? IdButiranPermohonan)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling MuatButiranPermohonan");

            try
            {
                // Call the service layer to retrieve Butiran Permohonan data
                var result = await _butiranpermohonanext.MuatButiranPermohonan(IdPermohonanJawatan, IdButiranPermohonan);

                // Return HTTP 200 OK with the retrieved result
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Initialize an empty string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in MuatButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP POST requests to deactivate (mansuh) a specific Butiran Jawatan record.
        //          Logs the operation, delegates to the service layer, and returns success or error response.

        /// <summary>
        /// Deactivates a specific Butiran Jawatan record based on the provided request.
        /// </summary>
        /// <param name="request">The DTO containing the identifier and details of the Butiran Jawatan to be deactivated.</param>
        /// <returns>Returns HTTP 200 OK with a success message, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully deactivated the Butiran Jawatan record.</response>
        /// <response code="500">Internal server error occurred while processing the deactivation.</response>
        [HttpPost("mansuh-butiran-jawatan")] // Defines the route as POST /mansuh-butiran-jawatan
        public async Task<ActionResult> MansuhButiranButiranJawatan([FromBody] MansuhButiranJawatanRequestDto request)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling MansuhButiranButiranJawatan");

            try
            {
                // Call the service layer to process the deactivation request
                var data = await _butiranpermohonanext.MansuhButiranButiranJawatan(request);

                // Return HTTP 200 OK with a success message
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                // Initialize an empty string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in MansuhButiranButiranJawatan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP POST requests to transfer (pindah) Butiran Permohonan data.
        //          Logs the operation, delegates to the service layer, and returns a success or error response.

        /// <summary>
        /// Transfers Butiran Permohonan data to a new context or state.
        /// </summary>
        /// <param name="request">The DTO containing transfer details for the Butiran Permohonan.</param>
        /// <returns>Returns HTTP 200 OK with a success message, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully transferred Butiran Permohonan data.</response>
        /// <response code="500">Internal server error occurred during the transfer process.</response>
        [HttpPost("pindah-butiran")] // Defines the route as POST /pindah-butiran
        public async Task<ActionResult> PindahButiranPermohonan([FromBody] PindahButiranPermohonanRequestDto request)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling PindahButiranPermohonan");

            try
            {
                // Call the service layer to process the transfer request
                await _butiranpermohonanext.PindahButiranPermohonan(request);

                // Return HTTP 200 OK with a success message
                return Ok(new { message = "Berjaya Pindah Butiran Permohonan" });
            }
            catch (Exception ex)
            {
                // Initialize an empty string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in PindahButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP POST requests to update specific change details (butir perubahan) in Butiran Permohonan.
        //          Logs the operation, delegates to the service layer, and returns a success or error response.

        /// <summary>
        /// Updates specific change details (butir perubahan) in an existing Butiran Permohonan record.
        /// </summary>
        /// <param name="request">The DTO containing the updated change details.</param>
        /// <returns>Returns HTTP 200 OK with a success message, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully updated the change details in Butiran Permohonan.</response>
        /// <response code="500">Internal server error occurred while processing the update.</response>
        [HttpPost("kemas-kini-butir-perubahan")] // Defines the route as POST /kemas-kini-butir-perubahan
        public async Task<ActionResult> KemaskiniButiranPerubahanButiranPermohonan([FromQuery] KemaskiniButiranPermohonanRequestDto request)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling KemaskiniButiranPerubahanButiranPermohonan");

            try
            {
                // Call the service layer to process the update request for change details
                await _butiranpermohonanext.KemaskiniButiranPerubahanButiranPermohonan(request);

                // Return HTTP 200 OK with a success message
                return Ok(new { message = "Berjaya Kemaskini Butir Perubahan" });
            }
            catch (Exception ex)
            {
                // Initialize an empty string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in KemaskiniButiranPerubahanButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }

        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP PATCH requests to calculate the financial implications (implikasi kewangan) of a Butiran Permohonan.
        //          Logs the operation, delegates to the service layer, and returns a Created response or error details.

        /// <summary>
        /// Calculates the financial implications (implikasi kewangan) for a given Butiran Permohonan.
        /// </summary>
        /// <param name="request">The DTO containing input parameters for financial calculation.</param>
        /// <returns>Returns HTTP 201 Created with a reference to the calculation, or HTTP 500 with error details.</returns>
        /// <response code="201">Successfully calculated financial implications.</response>
        /// <response code="500">Internal server error occurred during financial calculation.</response>
        [HttpPatch("kira-implikasi-kewangan")] // Defines the route as PATCH /kira-implikasi-kewangan
        public async Task<ActionResult> KiraImplikasiKewanganButiranPermohonan([FromBody] KiraImplikasiKewanganRequestDto request)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling KiraImplikasiKewanganButiranPermohonan");

            try
            {
                // Call the service layer to calculate financial implications based on the request
                await _butiranpermohonanext.KiraImplikasiKewanganButiranPermohonan(request);

                // Return HTTP 201 Created with a reference to this action and the original request payload
                return CreatedAtAction(nameof(KiraImplikasiKewanganButiranPermohonan), new { request }, null);
            }
            catch (Exception ex)
            {
                // Initialize an empty string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in KiraImplikasiKewanganButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP GET requests to retrieve a list of Butiran Permohonan records.
        //          Logs the operation, delegates to the service layer, and returns the result or error response.

        /// <summary>
        /// Retrieves a list of all Butiran Permohonan records.
        /// </summary>
        /// <returns>Returns HTTP 200 OK with the list of records, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully retrieved the list of Butiran Permohonan records.</response>
        /// <response code="500">Internal server error occurred while retrieving the data.</response>
        [HttpGet] // Defines the route as GET / (default route for this controller)
        public async Task<ActionResult<IEnumerable<TambahButiranPermohonanDto>>> SenaraiButiranPermohonan()
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling SenaraiButiranPermohonan");

            try
            {
                // Call the service layer to retrieve the list of Butiran Permohonan records
                var result = await _butiranpermohonanext.SenaraiButiranPermohonan();

                // Return HTTP 200 OK with the retrieved list
                return result;
            }
            catch (Exception ex)
            {
                // Initialize an empty string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in SenaraiButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP GET requests to retrieve a specific Butiran Permohonan record by its unique identifier.
        //          Logs the operation, delegates to the service layer, and returns the result or an error response.

        /// <summary>
        /// Retrieves a specific Butiran Permohonan record by its unique identifier.
        /// </summary>
        /// <param name="IdPermohonanJawatan">The unique identifier of the Butiran Permohonan record.</param>
        /// <returns>Returns HTTP 200 OK with the retrieved record, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully retrieved the Butiran Permohonan record.</response>
        /// <response code="500">Internal server error occurred while retrieving the record.</response>
        [HttpGet("{IdPermohonanJawatan}")] // Defines the route as GET /{IdPermohonanJawatan}, binding the route parameter to the method input
        public async Task<ActionResult<TambahButiranPermohonanDto>> BacaButiranPermohonan(int IdPermohonanJawatan)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling BacaButiranPermohonan");

            try
            {
                // Call the service layer to retrieve the Butiran Permohonan record by ID
                var result = await _butiranpermohonanext.BacaButiranPermohonan(IdPermohonanJawatan);

                // Return HTTP 200 OK with the retrieved record
                return result;
            }
            catch (Exception ex)
            {
                // Initialize a string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in BacaButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        [HttpGet("baca-butiran")] // Defines the route as GET /{IdPermohonanJawatan}, binding the route parameter to the method input
        public async Task<ActionResult<TambahButiranPermohonanDto>> BacaRekodButiranPermohonan(int Id)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling BacaRekodButiranPermohonan");

            try
            {
                // Call the service layer to retrieve the Butiran Permohonan record by ID
                var result = await _butiranpermohonanext.BacaRekodButiranPermohonan(Id);

                // Return HTTP 200 OK with the retrieved record
                return result;
            }
            catch (Exception ex)
            {
                // Initialize a string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in BacaRekodButiranPermohonan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Handles HTTP GET requests to retrieve Butiran Permohonan records filtered by Aktiviti Organisasi.
        //          Logs the operation, delegates to the service layer, and returns the result or error response.

        /// <summary>
        /// Retrieves Butiran Permohonan records filtered by Aktiviti Organisasi.
        /// </summary>
        /// <param name="IdPermohonanJawatan">The identifier used to filter records by Aktiviti Organisasi.</param>
        /// <returns>Returns HTTP 200 OK with the filtered list of records, or HTTP 500 with error details.</returns>
        /// <response code="200">Successfully retrieved filtered Butiran Permohonan records.</response>
        /// <response code="500">Internal server error occurred while retrieving the data.</response>
        [HttpGet("ikut-aktiviti-organisasi")] // Defines the route as GET /ikut-aktiviti-organisasi
        public async Task<ActionResult<List<ButiranPermohonanIkutAktivitiOrganisasiDto>>> ButiranPermohonanIkutAktivitiOrganisasi(int IdPermohonanJawatan)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling ButiranPermohonanIkutAktivitiOrganisasi");

            try
            {
                // Call the service layer to retrieve Butiran Permohonan records based on Aktiviti Organisasi
                var result = await _butiranpermohonanext.ButiranPermohonanIkutAktivitiOrganisasi(IdPermohonanJawatan);

                // Return HTTP 200 OK with the retrieved list
                return result;
            }
            catch (Exception ex)
            {
                // Initialize a string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in ButiranPermohonanIkutAktivitiOrganisasi");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        [HttpPost("tag-jawatan")] // Defines the route as GET /ikut-aktiviti-organisasi
        public async Task<ActionResult<List<ButiranPermohonanIkutAktivitiOrganisasiDto>>> ButiranPermohonanTagJawatan([FromBody] ButiranPermohonanTagJawatanRequestDto request)
        {
            // Log the start of the method execution for traceability
            _logger.LogInformation("Calling ButiranPermohonanIkutAktivitiOrganisasi");

            try
            {
                // Call the service layer to retrieve Butiran Permohonan records based on Aktiviti Organisasi
                var result = await _butiranpermohonanext.ButiranPermohonanTagJawatan(request);

                // Return HTTP 200 OK with the retrieved list
                return Ok( new {
                    status = result != null ? "Berjaya" : "Gagal",
                    items = result
                    });
            }
            catch (Exception ex)
            {
                // Initialize a string to hold inner exception message if available
                string err = "";

                // Check if the exception object is not null
                if (ex != null)
                {
                    // Log the error with full exception details
                    _logger.LogError(ex, "Error in ButiranPermohonanTagJawatan");

                    // If there's an inner exception, extract its message
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                // Return HTTP 500 Internal Server Error with combined exception messages
                return StatusCode(500, ex.Message + " - " + err);
            }
        }
    }
}
