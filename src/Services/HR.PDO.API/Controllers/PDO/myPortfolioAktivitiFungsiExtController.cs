using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/my-portfolio-aktiviti-fungsi")]
    public class myPortfolioAktivitiFungsiExtController : ControllerBase
    {
        private readonly ILogger<myPortfolioAktivitiFungsiExtController> _logger;
        private readonly ImyPortfolioAktivitiFungsiExt _myportfolioaktivitifungsiext;

        public myPortfolioAktivitiFungsiExtController(ImyPortfolioAktivitiFungsiExt myportfolioaktivitifungsiext, ILogger<myPortfolioAktivitiFungsiExtController> logger)
        {
            _myportfolioaktivitifungsiext = myportfolioaktivitifungsiext;
            _logger = logger;
        }

    }
}
