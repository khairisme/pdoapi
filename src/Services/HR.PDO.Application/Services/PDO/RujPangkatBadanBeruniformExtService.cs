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
using HR.PDO.Application.DTOs;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Net.Http;
using HR.PDO.Shared.Configuration;

namespace HR.PDO.Application.Services.PDO
{
    public class RujPangkatBadanBeruniformExtService : IRujPangkatBadanBeruniformExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujPangkatBadanBeruniformExtService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public RujPangkatBadanBeruniformExtService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings, IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujPangkatBadanBeruniformExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("PpaApi");
            _apiSettings = apiSettings.Value;
        }

        public async Task<List<DropDownDto>> RujukanPangkat()
        {
            try

            {

                var result = await (from pparpbb in _context.PPARujPangkatBadanBeruniform
                                    select new DropDownDto
                                    {
                                        Kod = pparpbb.Kod.Trim(),
                                        Nama = pparpbb.Nama.Trim()
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanPangkat");

                throw;
            }

        }


        public async Task<List<DropDownDto>> GetPangkatAsync()
        {
            var response = await _httpClient.GetAsync(_apiSettings.RujukanPangkatEndpoint);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<DropDownDto>>(json, options);
            return data;
        }

    }
}
