using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/kategori-soalan")]
    public class RujKategoriSoalanExtController : ControllerBase
    {
        private readonly ILogger<RujKategoriSoalanExtController> _logger;
        private readonly IRujKategoriSoalanExt _rujkategorisoalanext;

        public RujKategoriSoalanExtController(IRujKategoriSoalanExt rujkategorisoalanext, ILogger<RujKategoriSoalanExtController> logger)
        {
            _rujkategorisoalanext = rujkategorisoalanext;
            _logger = logger;
        }

    }
}
