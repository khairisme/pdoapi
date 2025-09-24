using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/bidang-pengkhususan")]
    public class RujukanBidangPengkhususanExtController : ControllerBase
    {
        private readonly ILogger<RujukanBidangPengkhususanExtController> _logger;
        private readonly IRujukanBidangPengkhususanExt _rujukanBidangPengkhususan;

        public RujukanBidangPengkhususanExtController(IRujukanBidangPengkhususanExt rujukanBidangPengkhususan, ILogger<RujukanBidangPengkhususanExtController> logger)
        {
            _rujukanBidangPengkhususan = rujukanBidangPengkhususan;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            _logger.LogInformation("Getting all RujJenisSaraan");

            var result = await _rujukanBidangPengkhususan.RujukanBidangPengkhususan();

            return Ok(new
            {
                status = result.Count() > 0 ? "Berjaya" : "Gagal",
                items = result

            });
        }

    }

}
