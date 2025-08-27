using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/penetapan-implikasi-kewangan")]
    public class PenetapanImplikasiKewanganExtController : ControllerBase
    {
        private readonly ILogger<PenetapanImplikasiKewanganExtController> _logger;
        private readonly IPenetapanImplikasiKewanganExt _penetapanimplikasikewanganext;

        public PenetapanImplikasiKewanganExtController(IPenetapanImplikasiKewanganExt penetapanimplikasikewanganext, ILogger<PenetapanImplikasiKewanganExtController> logger)
        {
            _penetapanimplikasikewanganext = penetapanimplikasikewanganext;
            _logger = logger;
        }

    }
}
