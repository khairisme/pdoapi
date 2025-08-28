using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-kategori-jawatan")]
    public class RujKategoriJawatanExtController : ControllerBase
    {
        private readonly ILogger<RujKategoriJawatanExtController> _logger;
        private readonly IRujKategoriJawatanExt _rujkategorijawatanext;

        public RujKategoriJawatanExtController(IRujKategoriJawatanExt rujkategorijawatanext, ILogger<RujKategoriJawatanExtController> logger)
        {
            _rujkategorijawatanext = rujkategorijawatanext;
            _logger = logger;
        }

    }
}
