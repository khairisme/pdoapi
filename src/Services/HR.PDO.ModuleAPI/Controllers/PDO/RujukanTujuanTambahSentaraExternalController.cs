using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/tujuan-tambah-sentara")]
    public class RujukanTujuanTambahSentaraExternalController : ControllerBase
    {
        private readonly ILogger<RujukanTujuanTambahSentaraExternalController> _logger;
        private readonly IRujTujuanTambahSentaraExt _rujtujuantambahsentaraext;

        public RujukanTujuanTambahSentaraExternalController(IRujTujuanTambahSentaraExt rujtujuantambahsentaraext, ILogger<RujukanTujuanTambahSentaraExternalController> logger)
        {
            _rujtujuantambahsentaraext = rujtujuantambahsentaraext;
            _logger = logger;
        }

    }
}
