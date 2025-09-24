using HR.PDO.Application.Interfaces.PDO;
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
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PDO
{
    public class PenetapanImplikasiKewanganExtService : IPenetapanImplikasiKewanganExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<PenetapanImplikasiKewanganExtService> _logger;

        public PenetapanImplikasiKewanganExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PenetapanImplikasiKewanganExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<ImplikasiKewanganDto> KosImplikasiKewangan(ImplikasiKewanganRequestDto request)
        {
            try

            {
                var result = new ImplikasiKewanganDto
                {
                    TotalKosSebulan = 0,
                    TotalKosSetahun = 0
                };
                foreach (var req in  request.ButiranPermohonanSkimGredList)
                {
                    var GredList = req.IdGredList
                        .Split(",")
                        .Select(s => int.TryParse(s.Trim(), out var val) ? val : 0)
                        .ToList();

                    var record = _context.PDOPenetapanImplikasiKewangan
                        .Where(ppik => GredList.Contains((int)ppik.IdGred)
                                    && ppik.IdSkimPerkhidmatan==req.IdSkimPerkhidmatan)
                        .GroupBy(ppik => 1) // Grouping by a constant to allow aggregation
                        .Select(g => new ImplikasiKewanganDto
                        {
                            TotalKosSebulan = g.Sum(ppik => ppik.ImplikasiKosSebulan),
                            TotalKosSetahun = g.Sum(ppik => ppik.ImplikasiKosSetahun)
                        })
                        .FirstOrDefault();

                    result.TotalKosSebulan += (record?.TotalKosSebulan ?? 0) * request.BilanganJawatan;
                }

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiImplikasiKewangan");

                throw;
            }

        }

        public async Task<SenaraiImplikasiKewanganOutputDto> SenaraiImplikasiKewangan(SenaraiImplikasiKewanganRequestDto request)
        {
            try

            {
                var implikasi = new ImplikasiKewanganDto
                {
                    TotalKosSebulan = 0,
                    TotalKosSetahun = 0
                };
                var result = new SenaraiImplikasiKewanganOutputDto();
                var gredStr = "";
                int highestId = 0;
                foreach (var req in request.ButiranPermohonanSkimGredList)
                {

                    var GredList = req.IdGredList
                      .Split(",")
                      .Select(s => int.TryParse(s.Trim(), out var val) ? val : 0)
                      .ToList();
                    int cnt = 0;

                    var GrdList = (from pdog in _context.PDOGred
                               where GredList.Contains((int)pdog.Id)
                               
                               select new GredDto
                               {
                                   Id = pdog.Id,
                                   code = pdog.Nama.Trim()
                               }
                            ).ToList();
                    var highestItem = GrdList
                        .OrderByDescending(x =>
                            int.TryParse(x.code.Substring(1), out var num) ? num : 0)
                        .FirstOrDefault();

                    var highestCode = highestItem?.code;
                    highestId = (int)highestItem?.Id;

                    foreach (var g in GrdList)
                    {
                        if (gredStr == "")
                        {
                            gredStr = g.code;
                        }
                        else
                        {
                            gredStr = gredStr + "," + g.code.Trim();
                        }
                    }
                    ++cnt;
                    var highest = GrdList
                        .OrderByDescending(code => int.Parse(code.code.Substring(1)))
                        .First();

                    if (cnt == GredList.Count())
                    {
                        gredStr = gredStr;
                    }
                    var newGred = gredStr.Replace(",", "/");
                    var newAnggaran = request.AnggaranBerkenaanTajukJawatan.Replace(newGred, "");
                    var record = _context.PDOPenetapanImplikasiKewangan
                        .Where(ppik => ppik.IdGred == highestId
                                    && ppik.IdSkimPerkhidmatan == req.IdSkimPerkhidmatan)
                        .GroupBy(ppik => 1) // Grouping by a constant to allow aggregation
                        .Select(g => new ImplikasiKewanganDto
                        {
                            TotalKosSebulan = g.Sum(ppik => ppik.ImplikasiKosSebulan),
                            TotalKosSetahun = g.Sum(ppik => ppik.ImplikasiKosSetahun)
                        })
                        .FirstOrDefault();


                    implikasi.TotalKosSebulan += (record?.TotalKosSebulan ?? 0) * request.BilanganJawatan;
                    result = new SenaraiImplikasiKewanganOutputDto
                    {
                        SkimPerkhidmatan = newAnggaran,
                        Gred = newGred,
                        BilanganAJawatan = request.BilanganJawatan,
                        ImplikasiKewanganSebulan = implikasi.TotalKosSebulan,
                        JumlahKewanganSebulan = implikasi.TotalKosSebulan * request.BilanganJawatan,
                        ImplikasiKewanganSetahun = implikasi.TotalKosSebulan * request.BilanganJawatan * 12
                    };
                }




                return result;
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiImplikasiKewangan");

                throw;
            }

        }


    }

}

