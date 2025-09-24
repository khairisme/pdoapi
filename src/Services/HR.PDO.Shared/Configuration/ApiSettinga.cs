namespace HR.PDO.Shared.Configuration
{
    public class ApiSettings
    {
        public string PpaApiBaseUrl { get; set; }
        public string OnbApiBaseUrl { get; set; }
        public string RujukanPangkatEndpoint { get; set; } = string.Empty;
        public string ExternalSenaraiNegaraEndpoint { get; set; } = string.Empty;
        public string ExternalSenaraiNegeriEndpoint { get; set; } = string.Empty;
        public string ExternalSenaraiBandarEndpoint { get; set; } = string.Empty;
        public string ExternalSenaraiSandanganEndpoint { get; set; } = string.Empty;
        public string ExternalSenaraiPemilikKompetensiEndpoint { get; set; } = string.Empty;
    }
}
