using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PPA;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using HR.PDO.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HR.Application.Services.PPA
{
    public class RujBandarExtService : IRujBandarExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujBandarExtService> _logger;
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;


        public RujBandarExtService(HttpClient httpClient, ApiSettings apiSettings, IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujBandarExtService> logger)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> SenaraiBandar(BandarRequestDto request)
        {
            try
            {
                var baseUrl = _apiSettings.PpaApiBaseUrl.TrimEnd('/');
                var endpoint = _apiSettings.ExternalSenaraiBandarEndpoint;

                // Build query string manually
                var query = $"?kodRujNegeri={Uri.EscapeDataString(request.kodRujNegeri)}&kodRujNegara={Uri.EscapeDataString(request.kodRujNegara)}";
                var url = $"{baseUrl}{endpoint}{query}";

                var response = await _httpClient.GetAsync(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<DropDownDto>>(json, options);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SenaraiNegeri");
                var err = ex.InnerException?.Message ?? "";
                throw new Exception(ex.Message + " - " + err);
            }
        }

    }

}

