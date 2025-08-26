using Shared.Messaging.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
