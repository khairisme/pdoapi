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
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;
using Azure.Core;
using HR.PDO.Application.DTOs.PDO;

namespace HR.Application.Services.PDO
{
    public class GredExtService : IGredExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<GredExtService> _logger;

        public GredExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<GredExtService> logger)
        {
            _context = dbContext;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanGredIkutKlasifikasiDanKumpulan(int IdKlasifikasiPerkhidmatan, int IdKumpulanPerkhidmatan)
        {
            try

            {

                var result = await (from pdog in _context.PDOGred
                    where pdog.IdKlasifikasiPerkhidmatan == IdKlasifikasiPerkhidmatan && pdog.IdKumpulanPerkhidmatan == IdKumpulanPerkhidmatan
                    select new DropDownDto{
                         Id = pdog.Id,
                        Kod = pdog.Kod.Trim(),
                         Nama = pdog.Nama.Trim()
                    }
                )
                .AsNoTracking()
                .ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGredIkutKlasifikasiDanKumpulan");

                throw;
            }

        }
        public async Task<List<DropDownDto>> RujukanGredIkutSkimPerkhidmatan(GredSkimRequestDto request)
        {
            try

            {

                var result = await (from pdogsp in _context.PDOGredSkimPerkhidmatan
                                    join pdog in _context.PDOGred on pdogsp.IdGred equals pdog.Id
                                    join pdosp in _context.PDOSkimPerkhidmatan on pdogsp.IdSkimPerkhidmatan equals pdosp.Id
                                    join pdorjs in _context.PDORujJenisSaraan on pdosp.KodRujJenisSaraan equals pdorjs.Kod
                                    where
                                     (pdorjs.Kod.Trim() == request.KodRujJenisSaraan || request.KodRujJenisSaraan == null)
                                     && (pdosp.IdKumpulanPerkhidmatan == request.IdKumpulanPerkhidmatan || request.IdKumpulanPerkhidmatan == 0)
                                     && (pdosp.IdKlasifikasiPerkhidmatan == request.IdKlasifikasiPerkhidmatan || request.IdKlasifikasiPerkhidmatan == 0)
                                     &&
                                     pdog.StatusAktif == true
                                    && pdogsp.IdSkimPerkhidmatan == request.IdSkimPerkhidmatan 
                                    select new DropDownDto
                                    {
                                        Id = pdog.Id,
                                        Kod = pdog.Kod.Trim(),
                                        Nama = pdog.Nama.Trim()
                                    }
                )
                .AsNoTracking()
                .Distinct()
                .ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGredIkutKlasifikasiDanKumpulan");

                throw;
            }

        }

        public async Task<List<DropDownDto>> RujukanGred()
        {
            try

            {

                var result = await (from pdog in _context.PDOGred
                                    select new DropDownDto
                                    {
                                        Id = pdog.Id,
                                        Kod = pdog.Kod.Trim(),
                                        Nama = pdog.Nama.Trim()
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGredIkutKlasifikasiDanKumpulan");

                throw;
            }

        }
        public async Task<List<DropDownDto>> RujukanGredKUP()
        {
            try

            {
                string?[] kup = new string?[] { "N1", "N2", "N3", "N4" };

                var result = await (from pdog in _context.PDOGred
                                    where kup.Contains(pdog.Nama)
                                          && pdog.StatusAktif == true
                                          && pdog.IndikatorGredLantikanTerus == true
                                    group pdog by pdog.Nama into g
                                    select new DropDownDto
                                    {
                                        Id = g.First().Id,
                                        Kod = g.First().Kod.Trim(),
                                        Nama = g.Key.Trim()
                                    }).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGredIkutKlasifikasiDanKumpulan");

                throw;
            }

        }


    }

}

