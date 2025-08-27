using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/permohonan-pengisian-skim")]
    public class PermohonanPengisianSkimExtController : ControllerBase
    {
        private readonly ILogger<PermohonanPengisianSkimExtController> _logger;
        private readonly IPermohonanPengisianSkimExt _permohonanpengisianskimext;

        public PermohonanPengisianSkimExtController(IPermohonanPengisianSkimExt permohonanpengisianskimext, ILogger<PermohonanPengisianSkimExtController> logger)
        {
            _permohonanpengisianskimext = permohonanpengisianskimext;
            _logger = logger;
        }

    }
}
