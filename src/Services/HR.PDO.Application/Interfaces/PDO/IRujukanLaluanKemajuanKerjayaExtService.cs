using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;

using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujukanLaluanKemajuanKerjayaExt
    {
        public Task<List<DropDownDto>> RujukanLaluanKemajuanKerjaya();
    }
}
