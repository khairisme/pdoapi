using HR.PDO.API;
using HR.PDO.API.Configuration;
using HR.PDO.API.Middleware;
using HR.PDO.API.Models;
using HR.PDO.API.Services;
using HR.PDO.Application;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure;
using HR.PDO.Infrastructure.Data.EntityFramework;
using HR.PDO.Shared;
using HR.PDO.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;                 // general MVC types
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // for AddControllers()
using Microsoft.Extensions.DependencyInjection;
//using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Shared.Messaging.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JwtDto>(builder.Configuration.GetSection("JwtSettings"));
// Add services to the container.
builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.PropertyNamingPolicy = null; // preserve property names
           options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
       });

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.Configure<KeycloakSettings>(builder.Configuration.GetSection("KeyCloak"));

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register HttpClient with base URL from settings
//builder.Services.AddHttpClient("PpaAPI", (sp, client) =>
//{
//    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
//    client.BaseAddress = new Uri(settings.PpaApiBaseUrl);
//    client.DefaultRequestHeaders.Accept.Add(
//        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//});
builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<ApiSettings>>().Value);

builder.Services.AddHttpClient("PpaApi", (sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.PpaApiBaseUrl);
});

builder.Services.AddHttpClient<KeyCloakService>();
var baseUrl = builder.Configuration["ApiSettings:PdpApiBaseUrl"];
builder.Services.AddHttpClient<IPermohonanPengisianService, PermohonanPengisianService>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(baseUrl));


builder.Services.AddSharedApplication();

// Register the Entity Framework implementation
builder.Services.AddEntityFrameworkInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor(); // Required to access HttpContext

builder.Services.AddDbContext<PNSDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PNSConnection"));
});

builder.Services.AddDbContext<PDODbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PDOConnection"));
});

builder.Services.AddDbContext<HRDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("HRConnection")));

builder.Services.AddDbContext<PDPDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PDPConnection")));

builder.Services.AddScoped<IPNSUnitOfWork, EfPNSUnitOfWork>();
builder.Services.AddScoped<IPDOUnitOfWork, EfPDOUnitOfWork>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<IPDPUnitOfWork, EfPDPUnitOfWork>();

// Add application services
builder.Services.AddApplication();
//builder.Services.AddModuleApi();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<PDODbContext>(opt =>
{
    //opt.EnableSensitiveDataLogging()
    //   .EnableDetailedErrors()
    //   .LogTo(Console.WriteLine, LogLevel.Information);
    opt.EnableSensitiveDataLogging()
       .EnableDetailedErrors()
       .LogTo(Console.WriteLine,
              new[] { DbLoggerCategory.Database.Command.Name },
              LogLevel.Information);
});


// Add RabbitMQ message bus
builder.Services.AddRabbitMQMessageBus(
    builder.Configuration["RabbitMQ:HostName"] ?? "rabbitmq",
    builder.Configuration["RabbitMQ:UserName"] ?? "guest",
    builder.Configuration["RabbitMQ:Password"] ?? "guest");

// Add Temporal services
builder.Services.AddTemporalServices(builder.Configuration);

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "HR.PDO API",
//        Version = "v1",
//        Description = "Endpoints for Permohonan Jawatan, Unit Organisasi, etc."
//    });

//    // Include XML comments for all assemblies
//    foreach (var xml in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml"))
//    {
//        c.IncludeXmlComments(xml, includeControllerXmlComments: true);
//    }

//    // Enable annotations for [SwaggerOperation], etc.
//    c.EnableAnnotations();
//});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "HR API", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Ensure all controllers are discovered
    c.EnableAnnotations();
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation($"Running on: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}");


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR API v1");
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR.PDO API v1");
    //c.DocumentTitle = "HR.PDO API Docs";
    //c.DisplayOperationId();
    //c.DefaultModelsExpandDepth(-1); // hide schema models panel (optional)
});
//}

app.UseHttpsRedirection();

app.UseCors("AllowAll");


//app.UseMiddleware<JwtMiddleware>();


app.UseAuthorization();


app.MapControllers();

app.Run();
