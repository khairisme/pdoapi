using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-jenis-saraan")]
    public class RujJenisSaraanExtController : ControllerBase
    {
        private readonly ILogger<RujJenisSaraanExtController> _logger;
        private readonly IRujJenisSaraanExt _rujjenissaraanext;

        public RujJenisSaraanExtController(IRujJenisSaraanExt rujjenissaraanext, ILogger<RujJenisSaraanExtController> logger)
        {
            _rujjenissaraanext = rujjenissaraanext;
            _logger = logger;
        }

    }
}
