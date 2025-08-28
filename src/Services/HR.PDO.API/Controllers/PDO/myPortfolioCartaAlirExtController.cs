using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/my-portfolio-carta-alir")]
    public class myPortfolioCartaAlirExtController : ControllerBase
    {
        private readonly ILogger<myPortfolioCartaAlirExtController> _logger;
        private readonly ImyPortfolioCartaAlirExt _myportfoliocartaalirext;

        public myPortfolioCartaAlirExtController(ImyPortfolioCartaAlirExt myportfoliocartaalirext, ILogger<myPortfolioCartaAlirExtController> logger)
        {
            _myportfoliocartaalirext = myportfoliocartaalirext;
            _logger = logger;
        }

    }
}
