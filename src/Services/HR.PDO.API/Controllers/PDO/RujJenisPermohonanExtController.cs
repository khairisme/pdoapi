using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-jenis-permohonan")]
    public class RujJenisPermohonanExtController : ControllerBase
    {
        private readonly ILogger<RujJenisPermohonanExtController> _logger;
        private readonly IRujJenisPermohonanExt _rujjenispermohonanext;

        public RujJenisPermohonanExtController(IRujJenisPermohonanExt rujjenispermohonanext, ILogger<RujJenisPermohonanExtController> logger)
        {
            _rujjenispermohonanext = rujjenispermohonanext;
            _logger = logger;
        }

    }
}
