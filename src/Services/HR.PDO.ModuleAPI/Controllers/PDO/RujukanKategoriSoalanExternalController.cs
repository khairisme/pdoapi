using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/kategori-soalan")]
    public class RujukanKategoriSoalanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanKategoriSoalanExternalController> _logger;
        private readonly IRujKategoriSoalanExt _rujkategorisoalanext;

        public RujukanKategoriSoalanExternalController(IRujKategoriSoalanExt rujkategorisoalanext, ILogger<RujukanKategoriSoalanExternalController> logger)
        {
            _rujkategorisoalanext = rujkategorisoalanext;
            _logger = logger;
        }

    }
}
