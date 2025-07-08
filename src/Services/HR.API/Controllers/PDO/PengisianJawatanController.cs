using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers.PDO
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PengisianJawatanController : ControllerBase
    {
        private readonly IPengisianJawatanService _pengisianJawatanService;
        private readonly ILogger<PengisianJawatanController> _logger;

        public PengisianJawatanController(IPengisianJawatanService pengisianJawatanService, ILogger<PengisianJawatanController> logger)
        {
            _pengisianJawatanService = pengisianJawatanService;
            _logger = logger;
        }

        #region Public API Methods
       

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting Pengisian Jawatan with ID: {Id} using Entity Framework Core", id);

            var result = await _pengisianJawatanService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion

    }
}