using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/tujuan-tambah-sentara")]
    public class RujTujuanTambahSentaraExtController : ControllerBase
    {
        private readonly ILogger<RujTujuanTambahSentaraExtController> _logger;
        private readonly IRujTujuanTambahSentaraExt _rujtujuantambahsentaraext;

        public RujTujuanTambahSentaraExtController(IRujTujuanTambahSentaraExt rujtujuantambahsentaraext, ILogger<RujTujuanTambahSentaraExtController> logger)
        {
            _rujtujuantambahsentaraext = rujtujuantambahsentaraext;
            _logger = logger;
        }

    }
}
