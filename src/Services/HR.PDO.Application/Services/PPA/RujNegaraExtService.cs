using Azure.Core;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
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
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;


namespace HR.Application.Services.PPA
{
    public class RujNegaraExtService : IRujNegaraExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujNegaraExtService> _logger;
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public RujNegaraExtService(HttpClient httpClient,ApiSettings apiSettings, IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujNegaraExtService> logger)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownNegaraDto>> SenaraiNegara()
        {
            try
            {
                var url = _apiSettings.PpaApiBaseUrl.Substring(0, _apiSettings.PpaApiBaseUrl.Length-2) +_apiSettings.ExternalSenaraiNegaraEndpoint;

                // Serialize the whole DTO
                var response = await _httpClient.GetAsync(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<DropDownNegaraDto>>(json, options);
                return data;

            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in SenaraiNegara");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }
                throw new Exception(ex.Message + "-" + err);
            }
        }


    }

}

