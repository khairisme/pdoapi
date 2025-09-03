using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/my-portfolio-carta-organisasi")]
    public class myPortfolioCartaOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<myPortfolioCartaOrganisasiExtController> _logger;
        private readonly ImyPortfolioCartaOrganisasiExt _myportfoliocartaorganisasiext;

        public myPortfolioCartaOrganisasiExtController(ImyPortfolioCartaOrganisasiExt myportfoliocartaorganisasiext, ILogger<myPortfolioCartaOrganisasiExtController> logger)
        {
            _myportfoliocartaorganisasiext = myportfoliocartaorganisasiext;
            _logger = logger;
        }

    }
}
