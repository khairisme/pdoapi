using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujGelaranJawatanExt
    {
        public Task<List<DropDownDto>> RujukanGelaranJawatan();
        public Task<int?> TambahGelaranJawatan(TambahGelaranJawatanRequestDto request);
        public Task<string?> BacaGelaranJawatan(string? KodRujGelaranJawatan);
    }
}
