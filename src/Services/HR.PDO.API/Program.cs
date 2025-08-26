using HR.PDO.Application;
using HR.PDO.Infrastructure;
using HR.PDO.Shared;
using HR.PDO.API.Configuration;
using Shared.Messaging.Extensions;
using HR.PDO.API.Models;
using HR.PDO.API.Services;
using HR.PDO.API.Middleware;
using HR.PDO.Application.Services.PDO;
using HR.PDO.API;
using HR.PDO.Application.Interfaces.PDO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtDto>(builder.Configuration.GetSection("JwtSettings"));
// Add services to the container.
builder.Services.AddControllers();

// Add services to the container.
builder.Services.Configure<KeycloakSettings>(builder.Configuration.GetSection("KeyCloak"));

builder.Services.AddHttpClient<KeyCloakService>();
var baseUrl = builder.Configuration["ApiSettings:PdpApiBaseUrl"];
builder.Services.AddHttpClient<IPermohonanPengisianService, PermohonanPengisianService>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(baseUrl));


builder.Services.AddSharedApplication();

// Register the default implementation (used by regular EmployeesController)
builder.Services.AddInfrastructure(builder.Configuration);

// Register the Entity Framework implementation
builder.Services.AddEntityFrameworkInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor(); // Required to access HttpContext

// Add application services
builder.Services.AddApplication();

builder.Services.AddAuthorization();


// Add RabbitMQ message bus
builder.Services.AddRabbitMQMessageBus(
    builder.Configuration["RabbitMQ:HostName"] ?? "rabbitmq",
    builder.Configuration["RabbitMQ:UserName"] ?? "guest",
    builder.Configuration["RabbitMQ:Password"] ?? "guest");

// Add Temporal services
builder.Services.AddTemporalServices(builder.Configuration);

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
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

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR API v1");
    });
//}

app.UseHttpsRedirection();

app.UseCors("AllowAll");


//app.UseMiddleware<JwtMiddleware>();


app.UseAuthorization();


app.MapControllers();

app.Run();
