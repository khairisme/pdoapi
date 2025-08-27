using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/skim-ketua-perkhidmatan")]
    public class SkimKetuaPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<SkimKetuaPerkhidmatanExtController> _logger;
        private readonly ISkimKetuaPerkhidmatanExt _skimketuaperkhidmatanext;

        public SkimKetuaPerkhidmatanExtController(ISkimKetuaPerkhidmatanExt skimketuaperkhidmatanext, ILogger<SkimKetuaPerkhidmatanExtController> logger)
        {
            _skimketuaperkhidmatanext = skimketuaperkhidmatanext;
            _logger = logger;
        }

    }
}
