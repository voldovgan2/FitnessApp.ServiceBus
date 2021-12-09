using Microsoft.Extensions.Options;
using NATS.Client;
using System;

namespace FitnessApp.AzzureServiceBus
{
    public class ServiceBus : IServiceBus
    {
        private IConnection _connection = null;

        public ServiceBus(IOptions<AzzureServiceBusSettings> settings)
        {
            var connectionFactory = new ConnectionFactory();
            _connection = connectionFactory.CreateConnection(settings.Value.Url);
        }

        public void PublishEvent(string subject, byte[] data)
        {
            _connection.Publish(subject, data);
        }

        public IAsyncSubscription SubscribeEvent(string subject, EventHandler<MsgHandlerEventArgs> handler)
        {
            return _connection.SubscribeAsync(subject, handler);
        }

        public void UnsubscribeEvent(IAsyncSubscription subscription)
        {
            subscription.Unsubscribe();
        }
    }
}
