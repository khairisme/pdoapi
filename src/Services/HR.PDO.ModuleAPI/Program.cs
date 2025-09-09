using HR.PDO.Application;
using HR.PDO.Infrastructure.Data.EntityFramework;
using HR.PDO.ModuleAPI;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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


builder.Services.AddHttpClient();

builder.Services.AddModuleApi();
builder.Services.AddApplication();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(HR.PDO.ModuleAPI.Controllers.KlasifikasiPerkhidmatanExternalController).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add RabbitMQ message bus
builder.Services.AddRabbitMQMessageBus(
    builder.Configuration["RabbitMQ:HostName"] ?? "rabbitmq",
    builder.Configuration["RabbitMQ:UserName"] ?? "guest",
    builder.Configuration["RabbitMQ:Password"] ?? "guest");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
