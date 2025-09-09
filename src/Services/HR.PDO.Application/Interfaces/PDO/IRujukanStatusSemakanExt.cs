using HR.PDO.Application.DTOs;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujukanStatusSemakanExt
    {
        public Task<List<DropDownDto>> RujukanStatusSemakan();
    }
}
