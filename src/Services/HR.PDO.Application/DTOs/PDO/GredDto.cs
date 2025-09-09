using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    /// <summary>
    /// DTO for filtering Gred records based on various criteria.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to filter Gred data in API requests.
    /// </remarks>
    public class GredFilterDto
    {
        /// <summary>
        /// Optional. ID of the service group (Kumpulan Perkhidmatan).
        /// </summary>
        public int? IdKumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. ID of the service classification (Klasifikasi Perkhidmatan).
        /// </summary>
        public int? IdKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. Status code of the application request.
        /// </summary>
        public string? KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Optional. Name of the Gred.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional. Code for the salary type reference (Jenis Saraan).
        /// </summary>
        public string? KodRujJenisSaraan { get; set; }
    }
    /// <summary>
    /// DTO for returning Gred result details in a list or search result.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return Gred information in API responses or UI lists.
    /// </remarks>
    public class GredResultDto
    {
        /// <summary>
        /// Sequential number in the list.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier of the Gred.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Code of the Gred.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the Gred.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Additional description or notes for the Gred.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Status of the Gred application.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Current status of the Gred.
        /// </summary>
        public string StatusGred { get; set; }

        /// <summary>
        /// Optional. Code for the salary type reference (Jenis Saraan).
        /// </summary>
        public string? KodRujJenisSaraan { get; set; }
    }
    /// <summary>
    /// DTO for creating or updating Gred information.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to send Gred data when creating or updating Gred records via API.
    /// </remarks>
    public class CreateGredDto
    {
        /// <summary>
        /// Identifier of the Gred. Defaults to 0 for new records.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Code representing the salary type (Jenis Saraan) for the Gred.
        /// </summary>
        public string KodRujJenisSaraan { get; set; }

        /// <summary>
        /// Identifier of the service classification (Klasifikasi Perkhidmatan).
        /// </summary>
        public int IdKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Identifier of the service group (Kumpulan Perkhidmatan).
        /// </summary>
        public int IdKumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Code of the Gred.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the Gred.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Optional. Order or sequence of the Gred.
        /// </summary>
        public int? TurutanGred { get; set; }

        /// <summary>
        /// Gred code identifier.
        /// </summary>
        public string KodGred { get; set; }

        /// <summary>
        /// Number associated with the Gred.
        /// </summary>
        public int NomborGred { get; set; }

        /// <summary>
        /// Optional. Additional description or notes for the Gred.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Optional. Indicates whether the Gred allows direct appointment.
        /// </summary>
        public bool? IndikatorGredLantikanTerus { get; set; }

        /// <summary>
        /// Optional. Indicates whether the Gred allows regular appointments.
        /// </summary>
        public bool? IndikatorGredLantikan { get; set; }

        /// <summary>
        /// Indicates if the Gred is active.
        /// </summary>
        public bool StatusAktif { get; set; }

        /// <summary>
        /// Optional. Notes or details about the latest update.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Status code for the Gred application.
        /// </summary>
        public string? KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Optional. Reviewer’s comments or feedback.
        /// </summary>
        public string? UlasanPengesah { get; set; }
    }
    /// <summary>
    /// DTO for displaying the update details of a Gred, including classification and group information.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return the details of Gred updates and associated classification/group information.
    /// </remarks>
    public class PaparMaklumatGredButiranKemasDto
    {
        /// <summary>
        /// Optional. Details or notes regarding the latest update to the Gred.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Classification of the service associated with the Gred.
        /// </summary>
        public string? Klasifikasi { get; set; }

        /// <summary>
        /// Optional. Service group associated with the Gred.
        /// </summary>
        public string? Kumpulan { get; set; }
    }
    /// <summary>
    /// DTO for displaying detailed information about a Gred, including classification, group, status, and update details.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return complete Gred information for UI display or API responses.
    /// </remarks>
    public class PaparMaklumatGredDto
    {
        /// <summary>
        /// Unique identifier of the Gred record.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Reference code for the type of remuneration.
        /// </summary>
        public string KodRujJenisSaraan { get; set; }

        /// <summary>
        /// Classification of the service.
        /// </summary>
        public string Klasifikasi { get; set; }

        /// <summary>
        /// Identifier for the service classification.
        /// </summary>
        public int? IdKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Identifier for the service group.
        /// </summary>
        public int? IdKumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Name of the service group.
        /// </summary>
        public string Kumpulan { get; set; }

        /// <summary>
        /// Code of the Gred.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the Gred.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Optional. Ordering of the Gred.
        /// </summary>
        public int? TurutanGred { get; set; }

        /// <summary>
        /// Code representing the Gred.
        /// </summary>
        public string KodGred { get; set; }

        /// <summary>
        /// Gred number, serialized as a padded string (e.g., 08).
        /// </summary>
        [JsonConverter(typeof(IntToPaddedStringConverter))]
        public int? NomborGred { get; set; }

        /// <summary>
        /// Optional. Description or notes about the Gred.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Optional. Indicates if the Gred allows direct appointment.
        /// </summary>
        public bool? IndikatorGredLantikanTerus { get; set; }

        /// <summary>
        /// Optional. Indicates if the Gred allows appointment through normal channels.
        /// </summary>
        public bool? IndikatorGredLantikan { get; set; }

        /// <summary>
        /// Status of the Gred record (active/inactive).
        /// </summary>
        public bool StatusAktif { get; set; }

        /// <summary>
        /// Code representing the status of the Gred application.
        /// </summary>
        public string KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Display name of the Gred application status.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Optional. Timestamp of the last update to the Gred record.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }
    }

    /// <summary>
    /// JSON converter to serialize integers as padded strings (e.g., 8 → "08") 
    /// and deserialize padded strings back to integers (e.g., "08" → 8).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Ensures consistent formatting for integer values that need to be represented with leading zeros in JSON.
    /// </remarks>
    public class IntToPaddedStringConverter : JsonConverter<int>
    {
        /// <summary>
        /// Reads a padded string from JSON and converts it to an integer.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">Serializer options.</param>
        /// <returns>The integer value parsed from the string.</returns>
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Convert "08" → 8 when reading from JSON
            return int.Parse(reader.GetString());
        }

        /// <summary>
        /// Writes an integer value as a padded string to JSON.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The integer value to write.</param>
        /// <param name="options">Serializer options.</param>
        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            // Convert 8 → "08" when writing to JSON
            writer.WriteStringValue(value.ToString("D2"));
        }
    }

}
