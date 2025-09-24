using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/tujuan-tambah-sentara")]
    public class RujTujuanTambahSentaraExtController : ControllerBase
    {
        private readonly ILogger<RujTujuanTambahSentaraExtController> _logger;
        private readonly IRujTujuanTambahSentaraExt _rujtujuantambahsentaraext;

        public RujTujuanTambahSentaraExtController(IRujTujuanTambahSentaraExt rujtujuantambahsentaraext, ILogger<RujTujuanTambahSentaraExtController> logger)
        {
            _rujtujuantambahsentaraext = rujtujuantambahsentaraext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanTujuanTambahSentara()
        {
            _logger.LogInformation("Calling RujukanTujuanTambahSentara");
            try
            {
                var data = await _rujtujuantambahsentaraext.RujukanTujuanTambahSentara();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanTujuanTambahSentara");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
