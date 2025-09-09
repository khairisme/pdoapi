using HR.PDO.Application.Interfaces.PPA;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PPA;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Net.Http;
using HR.PDO.Shared.Configuration;
using System.Text;
using HR.PDO.Application.DTOs;

namespace HR.PDO.Application.Services.PDO
{
    public class SandanganExtService : ISandanganExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<SandanganExtService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public SandanganExtService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings, IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<SandanganExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("PpaApi");
            _apiSettings = apiSettings.Value;
        }

        public async Task<List<SandanganOutputDto>> SenaraiSandangan(JawatanListDto request)
        {
            try

            {

                var result = await (from onbs in _context.ONBSandangan
                                    where request.IdJawatanList.Contains(onbs.IdJawatan)
                                    select new SandanganOutputDto
                                    {
                                        Id = onbs.Id,
                                        IdPemilikKompetensi = onbs.IdPemilikKompetensi,
                                        IdJawatan = onbs.IdJawatan,
                                        TarikhMulaSandangan = onbs.TarikhMulaSandangan,
                                        TarikhTamatSandangan = onbs.TarikhTamatSandangan,
                                        IdCipta = onbs.IdCipta,
                                    }
                                    ).ToListAsync();
                return result;
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiSandangan");

                throw;
            }

        }


        public async Task<List<SandanganOutputDto>> GetSandanganAsync(JawatanListDto request)
        {
            var url = _apiSettings.ExternalSenaraiPemilikKompetensiEndpoint;

            // Serialize the whole DTO
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST
            var response = await _httpClient.PostAsync(url, content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<SandanganOutputDto>>(json, options);
            return data;
        }

    }
}
