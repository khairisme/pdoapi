using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using  Shared.Contracts.DTOs;
using Swashbuckle.AspNetCore.Annotations;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/jenis-permohonan")]
    public class RujJenisPermohonanExtController : ControllerBase
    {
        private readonly ILogger<RujJenisPermohonanExtController> _logger;
        private readonly IRujJenisPermohonanExt _rujjenispermohonanext;

        public RujJenisPermohonanExtController(IRujJenisPermohonanExt rujjenispermohonanext, ILogger<RujJenisPermohonanExtController> logger)
        {
            _rujjenispermohonanext = rujjenispermohonanext;
            _logger = logger;
        }
  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanJenisPermohonan()
        {
            _logger.LogInformation("Calling RujukanJenisPermohonan");
            try
            {
                var data = await _rujjenispermohonanext.RujukanJenisPermohonan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in RujukanJenisPermohonan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

    }
}
