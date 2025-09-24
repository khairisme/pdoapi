using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces;
using HR.PDO.Application.Services;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.PDO.API.Controllers;

[ApiController]
[Route("api/pns/[controller]")]
public class PenggunaController : ControllerBase
{
    private readonly IPenggunaService _penggunaService;
    private readonly ILogger<PenggunaController> _logger;

    public PenggunaController(IPenggunaService penggunaService, ILogger<PenggunaController> logger)
    {
        _penggunaService = penggunaService;
        _logger = logger;
    }
    /// <summary>
    ///API to check the user's credential with temporary password.
    /// </summary>
    /// <param name="ValidateNewPengguna"></param>
    /// <returns></returns>
    [HttpPost("valNewPengguna")]
    public async Task<IActionResult> valNewPengguna([FromBody] ValidateNewPengguna validateNewPengguna)
    {
        _logger.LogInformation("Validating a new user");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _penggunaService.mengesahkankelayakanpengguna(validateNewPengguna.IdPengguna, validateNewPengguna.KataLaluanSementara    );

        return Ok(new
        {
            status = result
        });
    }

    /// <summary>
    ///API to set passwords.
    /// </summary>
    /// <param name="TetapkanKataLaluanDto"></param>
    /// <returns></returns>
    [HttpPost("setKatalaluan")]
    public async Task<IActionResult> setKatalaluan([FromBody] TetapkanKataLaluanDto tetapkanKataLaluanDto)
    {
        _logger.LogInformation("set user password");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _penggunaService.tetapkankatalaluan(tetapkanKataLaluanDto.IdPengguna, tetapkanKataLaluanDto.KataLaluanHash);

        return Ok(new
        {
            status = result
        });
    }

    /// <summary>
    ///Add Security Questions.
    /// </summary>
    /// <param name="SoalanKeselamatanDto"></param>
    /// <returns></returns>
    [HttpPost("newSoalanKeselamata")]
    public async Task<IActionResult> newSoalanKeselamata([FromBody] List<SoalanKeselamatanDto> soalanKeselamatanDto)
    {
        
        _logger.LogInformation("Add Security Questions for User ID {IdPengguna}", soalanKeselamatanDto!=null? soalanKeselamatanDto.FirstOrDefault().IdPengguna:"");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _penggunaService.CreateSoalanKeselamatanAsync(soalanKeselamatanDto);

        return Ok(new
        {
            status= result.Count()>0? "Sucess":"Failed",
            item = result
        });
    }
    /// <summary>
    ///Get all images
    /// </summary>
    /// <param name="GambarDto"></param>
    /// <returns></returns>
    [HttpGet("getAllGambar")]
    public async Task<IActionResult> getAllGambar()
    {
        _logger.LogInformation("Getting all Gambar");

        var gambars = await _penggunaService.GetAllGambarAsync();

        return Ok(new
        {
            status = gambars.Count() > 0 ? "Berjaya" : "Gagal",
            Verfication_photos = gambars
           
        });
    }
    /// <summary>
    ///set Verification Photo
    /// </summary>
    /// <param name="PerkhidmatanLogDto"></param>
    /// <returns></returns>
    [HttpPost("createPerkhidmatanLog")]
    public async Task<IActionResult> CreatePerkhidmatanLog([FromBody] PerkhidmatanLogDto perkhidmatanLog)
    {
        _logger.LogInformation("Creating a new PerkhidmatanLog");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _penggunaService.CreatePerkhidmatanAsync(perkhidmatanLog);

        return Ok(new
        {
            status = result

        });
    }

}
