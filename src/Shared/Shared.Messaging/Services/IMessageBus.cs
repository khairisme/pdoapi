using System;
using System.Threading.Tasks;

namespace Shared.Messaging.Services
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message, string topicName) where T : class;
    }
}
