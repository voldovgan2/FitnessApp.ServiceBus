using Azure.Messaging.ServiceBus;
using FitnessApp.ServiceBus.AzzureServiceBus.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FitnessApp.ServiceBus.AzzureServiceBus
{
    public abstract class MessageBase
    {
        protected readonly AzzureServiceBusSettings _azzureServiceBusSettings;
        protected readonly ServiceBusClient _serviceBusClient;
        protected readonly ILogger<MessageBase> _log;

        public MessageBase
        (
            IOptions<AzzureServiceBusSettings> azzureServiceBusSettings,
            ILogger<MessageBase> log
        )
        {
            _azzureServiceBusSettings = azzureServiceBusSettings.Value;
            _serviceBusClient = new ServiceBusClient(_azzureServiceBusSettings.ConnectionString);
            _log = log;
        }
    }
}