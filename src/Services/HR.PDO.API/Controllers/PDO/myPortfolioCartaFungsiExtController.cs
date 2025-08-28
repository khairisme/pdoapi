using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/my-portfolio-carta-fungsi")]
    public class myPortfolioCartaFungsiExtController : ControllerBase
    {
        private readonly ILogger<myPortfolioCartaFungsiExtController> _logger;
        private readonly ImyPortfolioCartaFungsiExt _myportfoliocartafungsiext;

        public myPortfolioCartaFungsiExtController(ImyPortfolioCartaFungsiExt myportfoliocartafungsiext, ILogger<myPortfolioCartaFungsiExtController> logger)
        {
            _myportfoliocartafungsiext = myportfoliocartafungsiext;
            _logger = logger;
        }

    }
}
