using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/profil-pemilik-kompetensi")]
    public class ProfilPemilikKompetensiExtController : ControllerBase
    {
        private readonly ILogger<ProfilPemilikKompetensiExtController> _logger;
        private readonly IProfilPemilikKompetensiExt _profilpemilikkompetensiext;

        public ProfilPemilikKompetensiExtController(
            IProfilPemilikKompetensiExt profilpemilikkompetensiext,
            ILogger<ProfilPemilikKompetensiExtController> logger)
        {
            _profilpemilikkompetensiext = profilpemilikkompetensiext;
            _logger = logger;
        }

        [HttpGet("carian")]
        [SwaggerOperation(
            Summary = "Carian Profil Pemilik Kompetensi mengikut Nama Pemilik Kompetensi dan Nombor Kad Pengenalan",
            Description = "Search Profil Pemilik Kompetensi using query filters (keyword, page, pageSize, sortBy, desc).",
            OperationId = "CarianProfilPemilikKompetensi",
            Tags = new[] { "ProfilPemilikKompetensiExt" }
        )]
        public async Task<ActionResult<IEnumerable<ProfilPemilikKompetensiDisplayDto>>> CarianProfilPemilikKompetensi(
            [FromQuery] ProfilPemilikKompetensiCarianDto request)
        {
            _logger.LogInformation("Calling CarianProfilPemilikKompetensi");
            try
            {
                var data = await _profilpemilikkompetensiext.CarianProfilPemilikKompetensi(request);
                return Ok(data); // empty list => 200 OK
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CarianProfilPemilikKompetensi");
                var inner = ex.InnerException?.Message;
                return StatusCode(500, string.IsNullOrEmpty(inner) ? ex.Message : $"{ex.Message} - {inner}");
            }
        }

        [HttpPost("External")]
        [SwaggerOperation(
            Summary = "Senarai Profil Pemilik Kompetensi",
            Description = "List of  Profil Pemilik Kompetensi.",
            OperationId = "SenaraiProfilPemilikKompetensi",
            Tags = new[] { "ProfilPemilikKompetensiExt" }
        )]
        public async Task<ActionResult<IEnumerable<SenaraiPemilikKompetensiOutputDto>>> SenaraiProfilPemilikKompetensi([FromBody] JawatanListDto request)
        {
            _logger.LogInformation("Calling SenaraiProfilPemilikKompetensi");
            try
            {
                var data = await _profilpemilikkompetensiext.SenaraiProfilPemilikKompetensi(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SenaraiProfilPemilikKompetensi");
                var inner = ex.InnerException?.Message;
                return StatusCode(500, string.IsNullOrEmpty(inner) ? ex.Message : $"{ex.Message} - {inner}");
            }
        }
    }
}