using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
using HR.PDO.Infrastructure.Data.EntityFramework;

namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/gred")]
    public class GredExtController : ControllerBase
    {
        private readonly ILogger<GredExtController> _logger;
        private readonly IGredExt _gredext;
        private readonly PDODbContext _context;

        public GredExtController(IGredExt gredext, ILogger<GredExtController> logger)
        {
            _gredext = gredext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanGred()
        {
            _logger.LogInformation("Calling RujukanGred");
            try
            {
                var data = await _gredext.RujukanGred();
                return Ok(data);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in RujukanGred");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }


        [HttpGet("ikut-klasifikasi-kumpulan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanGredIkutKlasifikasiDanKumpulan(int IdKlasifikasiPerkhidmatan, int IdKumpulanPerkhidmatan)
        {
            try

            {
                var result = await _gredext.RujukanGredIkutKlasifikasiDanKumpulan(IdKlasifikasiPerkhidmatan, IdKumpulanPerkhidmatan);

                return Ok(result);

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGredIkutKlasifikasiDanKumpulan");

                throw;
            }

        }

    }
}
