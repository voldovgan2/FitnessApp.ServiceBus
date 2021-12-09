using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.ServiceBus.IntegrationEvents;
using Microsoft.Extensions.Hosting;
using NATS.Client;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessApp.AzzureServiceBus
{
    public abstract class MessageBusService : IHostedService
    {
        private readonly IServiceBus _serviceBus;
        private IAsyncSubscription _eventSubscription;
        private readonly IJsonSerializer _serializer;

        public MessageBusService
        (
            IServiceBus serviceBus,
            IJsonSerializer serializer
        )
        {
            _serviceBus = serviceBus;
            _serializer = serializer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventSubscription = _serviceBus.SubscribeEvent(Topic.NEW_USER_REGISTERED, (sender, args) =>
            {
                var integrationEvent = _serializer.DeserializeFromBytes<NewUserRegisteredEvent>(args.Message.Data);
                HandleNewUserRegisteredEvent(integrationEvent);
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _eventSubscription.Unsubscribe();
            return Task.CompletedTask;
        }

        protected abstract void HandleNewUserRegisteredEvent(NewUserRegisteredEvent integrationEvent);
    }
}
