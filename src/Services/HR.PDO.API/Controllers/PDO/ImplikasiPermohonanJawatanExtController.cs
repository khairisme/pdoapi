using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/implikasi-permohonan-jawatan")]
    public class ImplikasiPermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<ImplikasiPermohonanJawatanExtController> _logger;
        private readonly IImplikasiPermohonanJawatanExt _implikasipermohonanjawatanext;

        public ImplikasiPermohonanJawatanExtController(IImplikasiPermohonanJawatanExt implikasipermohonanjawatanext, ILogger<ImplikasiPermohonanJawatanExtController> logger)
        {
            _implikasipermohonanjawatanext = implikasipermohonanjawatanext;
            _logger = logger;
        }

        //[HttpPost("tambah")]
        //public async Task<ActionResult> TambahImplikasiPermohonanJawatan([FromQuery] Guid UserId)
        //{
        //    _logger.LogInformation("Calling TambahImplikasiPermohonanJawatan");
        //    try
        //    {
        //        await _implikasipermohonanjawatanext.TambahImplikasiPermohonanJawatan(UserId,);
        //        return CreatedAtAction(nameof(TambahImplikasiPermohonanJawatan), new {UserId,  }, null);
        //        await _implikasipermohonanjawatanext.TambahButiranPermohonan(UserId,);
        //        return CreatedAtAction(nameof(TambahImplikasiPermohonanJawatan), new {UserId,  }, null);
        //    }
        //    catch (Exception ex)
        //    {
        //         String err = "";
        //         if (ex != null) { 
        //             _logger.LogError(ex, "Error in TambahButiranPermohonan");
        //             if (ex.InnerException!=null) {
        //                 err = ex.InnerException.Message.ToString();
        //             }
        //        }
                 
        //        return StatusCode(500, ex.Message+"-"+err);
        //    }
        //}

    }
}
