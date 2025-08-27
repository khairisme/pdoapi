using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-kategori-unit-organisasi")]
    public class RujKategoriUnitOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<RujKategoriUnitOrganisasiExtController> _logger;
        private readonly IRujKategoriUnitOrganisasiExt _rujkategoriunitorganisasiext;

        public RujKategoriUnitOrganisasiExtController(IRujKategoriUnitOrganisasiExt rujkategoriunitorganisasiext, ILogger<RujKategoriUnitOrganisasiExtController> logger)
        {
            _rujkategoriunitorganisasiext = rujkategoriunitorganisasiext;
            _logger = logger;
        }

    }
}
