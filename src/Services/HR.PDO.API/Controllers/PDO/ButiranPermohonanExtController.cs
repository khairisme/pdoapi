using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/butiran-permohonan")]
    public class ButiranPermohonanExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanExtController> _logger;
        private readonly IButiranPermohonanExt _butiranpermohonanext;

        public ButiranPermohonanExtController(IButiranPermohonanExt butiranpermohonanext, ILogger<ButiranPermohonanExtController> logger)
        {
            _butiranpermohonanext = butiranpermohonanext;
            _logger = logger;
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonan([FromQuery] Guid UserId, TambahButiranPermohonanDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonan");
            try
            {
                await _butiranpermohonanext.TambahButiranPermohonan(UserId,request);
                return CreatedAtAction(nameof(TambahButiranPermohonan), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahButiranPermohonan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
