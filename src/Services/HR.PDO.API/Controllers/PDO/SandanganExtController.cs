using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/sandangan")]
    public class SandanganExtController : ControllerBase
    {
        private readonly ILogger<SandanganExtController> _logger;
        private readonly ISandanganExt _SandanganExt;
        private readonly IHttpClientFactory httpClientFactory;

        public SandanganExtController(ISandanganExt SandanganExt, ILogger<SandanganExtController> logger)
        {
            _SandanganExt = SandanganExt;
            _logger = logger;
        }

        [HttpPost("call-pangkat")]
        public async Task<IActionResult> CallPangkatApi([FromBody] JawatanListDto request)
        {

            try
            {
                var data = await _SandanganExt.GetSandanganAsync(request);
                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in CallPangkatApi");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
            //var client = httpClientFactory.CreateClient("PpaAPI");

            //var response = await client.GetAsync("/api/pdo/v1/rujukan/pangkat"); // appsettings already contains base url
            //if (!response.IsSuccessStatusCode)
            //{
            //    return StatusCode((int)response.StatusCode, "Pangkat API failed");
            //}

            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //};


            //var json = await response.Content.ReadAsStringAsync();
            //var data = JsonSerializer.Deserialize<List<DropDownDto>>(json, options);

        }



        [HttpPost("Externa;")]
        public async Task<ActionResult<IEnumerable<SandanganOutputDto>>> SenaraiSandanganX([FromBody] JawatanListDto request) 
        {
            _logger.LogInformation("Calling SenaraiASandangan");
            try
            {
                var data = await _SandanganExt.SenaraiSandangan(request);
                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in SenaraiSandangan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }

    }
}
