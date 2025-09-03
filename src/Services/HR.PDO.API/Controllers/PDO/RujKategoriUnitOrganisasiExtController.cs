using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/kategori-unit-organisasi")]
    public class RujKategoriUnitOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<RujKategoriUnitOrganisasiExtController> _logger;
        private readonly IRujKategoriUnitOrganisasiExt _rujkategoriunitorganisasiext;

        public RujKategoriUnitOrganisasiExtController(IRujKategoriUnitOrganisasiExt rujkategoriunitorganisasiext, ILogger<RujKategoriUnitOrganisasiExtController> logger)
        {
            _rujkategoriunitorganisasiext = rujkategoriunitorganisasiext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKategoriUnitOrganisasi()
        {
            _logger.LogInformation("Calling RujukanKategoriUnitOrganisasi");
            try
            {
                var data = await _rujkategoriunitorganisasiext.RujukanKategoriUnitOrganisasi();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanKategoriUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
