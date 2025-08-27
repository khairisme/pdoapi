using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/kompetensi-jawatan-sebenar")]
    public class KompetensiJawatanSebenarExtController : ControllerBase
    {
        private readonly ILogger<KompetensiJawatanSebenarExtController> _logger;
        private readonly IKompetensiJawatanSebenarExt _kompetensijawatansebenarext;

        public KompetensiJawatanSebenarExtController(IKompetensiJawatanSebenarExt kompetensijawatansebenarext, ILogger<KompetensiJawatanSebenarExtController> logger)
        {
            _kompetensijawatansebenarext = kompetensijawatansebenarext;
            _logger = logger;
        }

    }
}
