using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/my-portfolio-senarai-semak")]
    public class myPortfolioSenaraiSemakExtController : ControllerBase
    {
        private readonly ILogger<myPortfolioSenaraiSemakExtController> _logger;
        private readonly ImyPortfolioSenaraiSemakExt _myportfoliosenaraisemakext;

        public myPortfolioSenaraiSemakExtController(ImyPortfolioSenaraiSemakExt myportfoliosenaraisemakext, ILogger<myPortfolioSenaraiSemakExtController> logger)
        {
            _myportfoliosenaraisemakext = myportfoliosenaraisemakext;
            _logger = logger;
        }

    }
}
