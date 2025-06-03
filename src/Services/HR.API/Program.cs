using HR.Application;
using HR.Infrastructure;
using HR.API.Configuration;
using Shared.Messaging.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register the default implementation (used by regular EmployeesController)
builder.Services.AddInfrastructure(builder.Configuration);

// Register the Entity Framework implementation
builder.Services.AddEntityFrameworkInfrastructure(builder.Configuration);


// Add application services
builder.Services.AddApplication();

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR API v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
