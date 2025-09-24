using System;
using System.ComponentModel;
namespace HR.PDO.Application.DTOs
{
    public class TambahGelaranJawatanRequestDto
    {
        public Guid? UserId { get; set; }
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? Keterangan { get; set; }
    }


}
