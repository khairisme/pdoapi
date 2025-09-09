using HR.PDO.API.Middleware;
using HR.PDO.Application.Interfaces;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Interfaces.PDP;
using HR.PDO.Application.Services;
using HR.PDO.Application.Services.PDO;
using HR.PDO.Application.Services.PDP;
using HR.Application.Services.PDO;
using Microsoft.Extensions.DependencyInjection;
using HR.PDO.Application.Interfaces.PPA;
using HR.Application.Services.PPA;
using Microsoft.EntityFrameworkCore;
using HR.PDO.Shared.Interfaces;


namespace HR.PDO.Application;

/// <summary>
/// Extension methods for setting up application services in an IServiceCollection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add application services to the specified IServiceCollection
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPenggunaService, PenggunaService>();

        services.AddScoped<IKumpulanPerkhidmatanService, KumpulanPerkhidmatanService>();
        services.AddScoped<IRujStatusPermohonanService, RujStatusPermohonanService>();
        services.AddScoped<IMaklumatKlasifikasiPerkhidmatanService, MaklumatKlasifikasiPerkhidmatanService>();

        services.AddScoped<IMaklumatSkimPerkhidmatanService, MaklumatSkimPerkhidmatanService>();
        services.AddScoped<IRujukanExt, RujukanExtService>();
        services.AddScoped<IRujKategoriUnitOrganisasiExt, RujKategoriUnitOrganisasiExtService>();
        services.AddScoped<IRujStatusJawatanExt, RujStatusJawatanExtService>();
        services.AddScoped<IRujJenisJawatanExt, RujJenisJawatanExtService>();
        services.AddScoped<IKumpulanPerkhidmatanExt, KumpulanPerkhidmatanExtService>();
        services.AddScoped<IKlasifikasiPerkhidmatanExt, KlasifikasiPerkhidmatanExtService>();
        services.AddScoped<IRujukanSkimPerkhidmatan, RujukanSkimPerkhidmatanService>();
        services.AddScoped<IGredExt, GredExtService>();
        services.AddScoped<ISkimKetuaPerkhidmatanExt, SkimKetuaPerkhidmatanExtService>();
        services.AddScoped<IRujGelaranJawatanExt, RujGelaranJawatanExtService>();
        services.AddScoped<IRujPangkatBadanBeruniformExt, RujPangkatBadanBeruniformExtService>();
        services.AddScoped<IPermohonanJawatanExt, PermohonanJawatanExtService>();
        services.AddScoped<IStatusPermohonanJawatanExt, StatusPermohonanJawatanExtService>();
        services.AddScoped<IDokumenPermohonanExt, DokumenPermohonanExtService>();
        services.AddScoped<IRujukanJenisDokumenExt, RujukanJenisDokumenExtService>();
        


        services.AddScoped<IGredService, GredService>();
        services.AddScoped<IRujJenisSaraanService, RujJenisSaraanService>();
        services.AddScoped<IJawatanService, JawatanService>();
        services.AddScoped<IKeyCloakService, KeyCloakService>();

        // Add JwtService
        services.AddScoped<JwtService>();

        services.AddScoped<IPermohonanJawatanService, PermohonanJawatanService>();

        services.AddScoped<IAktivitiOrganisasiService, AktivitiOrganisasiService>();
        services.AddScoped<IAktivitiOrganisasiExt, AktivitiOrganisasiExtService>();

        services.AddScoped<IRujJenisPermohonanService, RujJenisPermohonanService>();
        services.AddScoped<IPermohonanPeriwatanService, PermohonanPeriwatanService>();

        services.AddScoped<IPengisianJawatanService, PengisianJawatanService>();


       // services.AddScoped<IPermohonanPengisianService, PermohonanPengisianService>();
        services.AddScoped<IUnitOrganisasiService, UnitOrganisasiService>();
        services.AddScoped<IUnitOrganisasiExt, UnitOrganisasiExtService>();
        services.AddScoped<IRujStatusKekosonganJawatanService, RujStatusKekosonganJawatanService>();

        services.AddScoped<IPermohonanPengisianSkimService, PermohonanPengisianSkimService>();
        services.AddScoped<IJadualGajiService, JadualGajiService>();
        services.AddScoped<ISkimKetuaPerkhidmatanService, SkimKetuaPerkhidmatanService>();
        services.AddScoped<IButiranPermohonanExt, ButiranPermohonanExtService>();
        services.AddScoped<IButiranPermohonanJawatanExt, ButiranPermohonanJawatanExtService>();
        services.AddScoped<IButiranPermohonanExt, ButiranPermohonanExtService>();
        services.AddScoped<IButiranPermohonanSkimGredExt, ButiranPermohonanSkimGredExtService>();
        services.AddScoped<IButiranPermohonanJawatanExt, ButiranPermohonanJawatanExtService>();
        services.AddScoped<IButiranPermohonanSkimGredKUJExt, ButiranPermohonanSkimGredKUJExtService>();
        services.AddScoped<IButiranPermohonanSkimGredTBKExt, ButiranPermohonanSkimGredTBKExtService>();
        services.AddScoped<IPenetapanImplikasiKewanganExt, PenetapanImplikasiKewanganExtService>();
        services.AddScoped<ICadanganJawatanExt, CadanganJawatanExtService>();
        services.AddScoped<IRujKategoriJawatanExt, RujKategoriJawatanExtService>();
        services.AddScoped<IRujukanStatusPengesahan, RujukanStatusPengesahanService>();
        services.AddScoped<IObjectMapper, ReflectionObjectMapper>();
        services.AddScoped<ISandanganExt, SandanganExtService>();
        services.AddScoped<IJawatanExt, JawatanExtService>();
        services.AddScoped<IRujukanJenisSaraanExt, RujukanJenisSaraanExtService>();
        services.AddScoped<IProfilPemilikKompetensiExt, ProfilPemilikKompetensiExtService>();
        services.AddScoped<IRujukanAgensiExt, RujukanAgensiExtService>();
        services.AddScoped<IRujukanJenisAgensiExt, RujukanJenisAgensiExtService>();
        services.AddScoped<IRujukanPasukanPerundingExt, RujukanPasukanPerundingExtService>();
        


        return services;
    }
}
