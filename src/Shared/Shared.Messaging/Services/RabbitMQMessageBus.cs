using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Shared.Messaging.Services
{
    public class RabbitMQMessageBus : IMessageBus
    {
        private readonly ILogger<RabbitMQMessageBus> _logger;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;

        public RabbitMQMessageBus(
            ILogger<RabbitMQMessageBus> logger,
            string hostname,
            string username,
            string password)
        {
            _logger = logger;
            _hostname = hostname;
            _username = username;
            _password = password;
        }

        public Task PublishAsync<T>(T message, string topicName) where T : class
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: topicName, type: ExchangeType.Fanout);

                var jsonMessage = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                channel.BasicPublish(
                    exchange: topicName,
                    routingKey: "",
                    basicProperties: null,
                    body: body);

                _logger.LogInformation("Message published to {TopicName}: {Message}", topicName, jsonMessage);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message to {TopicName}", topicName);
                throw;
            }
        }
    }
}
