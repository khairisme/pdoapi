using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Interfaces.PPA;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
//using AutoMapper;
using HR.PDO.Shared.Configuration;
using HR.PDO.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace HR.Application.Services.PPA
{
    public class ProfilPemilikKompetensiExtService : IProfilPemilikKompetensiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<ProfilPemilikKompetensiExtService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        private readonly IObjectMapper _mapper;

        public ProfilPemilikKompetensiExtService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings, IObjectMapper mapper, IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ProfilPemilikKompetensiExtService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("PpaApi");
            _apiSettings = apiSettings.Value;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<ProfilPemilikKompetensiDisplayDto>> CarianProfilPemilikKompetensi(ProfilPemilikKompetensiCarianDto filter)
        {
            try

            {

                var result = (from pdoppk in _context.PPAProfilPemilikKompetensi
                              join pdog in _context.PDOGred on pdoppk.IdGred equals pdog.Id
                             where (filter.NomborKadPengenalan!=null && pdoppk.NomborKadPengenalan.Contains(filter.NomborKadPengenalan) || filter.NomborKadPengenalan == null)
                             && (filter.NamaPemilikKompetensi!=null && pdoppk.NamaPemilikKompetensi.Contains(filter.NamaPemilikKompetensi) || filter.NamaPemilikKompetensi==null)
                             select new ProfilPemilikKompetensiDisplayDto
                             {
                                 NomborKadPengenalan=pdoppk.NomborKadPengenalan,
                                 NamaPemilikKompetensi = pdoppk.NamaPemilikKompetensi,
                                 NamaSkimPerkhidmatan = pdoppk.NamaSkimPerkhidmatan,
                                 Gred = pdog.Nama
                             }).ToList();


                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in CarianSenaraiPermohonanJawatan");

                throw;
            }

        }

        public async Task<List<ProfilPemilikKompetensi>> SenaraiProfilPemilikKompetensi(JawatanListDto request)
        {
            try
            {
                var query = from p in _context.PPAProfilPemilikKompetensi
                where request.IdJawatanList.Contains(p.IdJawatan)
                            orderby p.NamaPemilikKompetensi
                             select p;
                var result = await query.ToListAsync();
                return result;
                              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SenaraiProfilPemilikKompetensi");
                throw;
            }
        }
        public async Task<List<ProfilPemilikKompetensi>> GetExternalSenaraiProfilPemilikKompetensiAsync(JawatanListDto request)
        {
            var url = _apiSettings.ExternalSenaraiPemilikKompetensiEndpoint;

            // Serialize the whole DTO
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));

            // Send POST
            var response = await _httpClient.PostAsync(url, content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<ProfilPemilikKompetensi>>(json, options);
            return data;
        }

    }

}

