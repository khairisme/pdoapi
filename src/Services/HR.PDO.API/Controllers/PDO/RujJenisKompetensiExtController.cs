using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-jenis-kompetensi")]
    public class RujJenisKompetensiExtController : ControllerBase
    {
        private readonly ILogger<RujJenisKompetensiExtController> _logger;
        private readonly IRujJenisKompetensiExt _rujjeniskompetensiext;

        public RujJenisKompetensiExtController(IRujJenisKompetensiExt rujjeniskompetensiext, ILogger<RujJenisKompetensiExtController> logger)
        {
            _rujjeniskompetensiext = rujjeniskompetensiext;
            _logger = logger;
        }

    }
}
