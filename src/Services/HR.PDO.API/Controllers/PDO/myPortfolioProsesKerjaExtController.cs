using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/my-portfolio-proses-kerja")]
    public class myPortfolioProsesKerjaExtController : ControllerBase
    {
        private readonly ILogger<myPortfolioProsesKerjaExtController> _logger;
        private readonly ImyPortfolioProsesKerjaExt _myportfolioproseskerjaext;

        public myPortfolioProsesKerjaExtController(ImyPortfolioProsesKerjaExt myportfolioproseskerjaext, ILogger<myPortfolioProsesKerjaExtController> logger)
        {
            _myportfolioproseskerjaext = myportfolioproseskerjaext;
            _logger = logger;
        }

    }
}
