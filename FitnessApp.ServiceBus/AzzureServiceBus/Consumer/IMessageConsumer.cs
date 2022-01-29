using System;
using System.Threading.Tasks;

namespace FitnessApp.ServiceBus.AzzureServiceBus.Consumer
{
    public interface IMessageConsumer
    {
        Task SubscribeMessage(string topic, string subscription, Func<string, Task> processMessage);
        Task UnsubscribeMessage();
    }
}
