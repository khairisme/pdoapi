using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for requesting Rujukan Skim Perkhidmatan (Service Scheme Reference).
    /// Author: Khairi bin Abu Bakar
    /// Date: 2025-09-04
    /// Purpose: Serves as the input model for filtering Skim Perkhidmatan 
    ///          based on Klasifikasi and Kumpulan criteria. 
    ///          Encapsulates request parameters to ensure clean method signatures 
    ///          and future extensibility.
    /// </summary>
    public class RujukanSkimPerkhidmatanRequestDto
    {
        /// <summary>
        /// Identifier for Klasifikasi Perkhidmatan (Service Classification).
        /// Nullable to allow flexible filtering.
        /// </summary>
        public int? IdKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Identifier for Kumpulan Perkhidmatan (Service Group).
        /// Nullable to allow flexible filtering.
        /// </summary>
        public int? IdKumpulanPerkhidmatan { get; set; }
        public string? KodRujJenisSaraan { get; set; }
    }
}
