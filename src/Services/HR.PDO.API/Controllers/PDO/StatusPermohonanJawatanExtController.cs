using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/status-permohonan-jawatan")]
    public class StatusPermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<StatusPermohonanJawatanExtController> _logger;
        private readonly IStatusPermohonanJawatanExt _statuspermohonanjawatanext;

        public StatusPermohonanJawatanExtController(IStatusPermohonanJawatanExt statuspermohonanjawatanext, ILogger<StatusPermohonanJawatanExtController> logger)
        {
            _statuspermohonanjawatanext = statuspermohonanjawatanext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusPermohonanJawatan(string KodRujPeranan)
        {
            _logger.LogInformation("Calling RujukanStatusPermohonanJawatan");
            try
            {
                var data = await _statuspermohonanjawatanext.RujukanStatusPermohonanJawatan(KodRujPeranan);
                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
