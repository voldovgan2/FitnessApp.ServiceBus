using Azure.Messaging.ServiceBus;
using FitnessApp.Common.Logger;
using FitnessApp.ServiceBus.AzzureServiceBus.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace FitnessApp.ServiceBus.AzzureServiceBus.Producer
{
    public class MessageProducer : MessageBase, IMessageProducer
    {
        public MessageProducer
        (
            IOptions<AzzureServiceBusSettings> azzureServiceBusSettings,
            ILogger<MessageProducer> log
        )
            : base(azzureServiceBusSettings, log)
        {
        }

        public async Task SendMessage(string topic, string data)
        {
            var sender = _serviceBusClient.CreateSender(topic);
            try
            {
                var message = new ServiceBusMessage(data)
                {
                    Subject = topic,
                    MessageId = Guid.NewGuid().ToString()
                };
                await sender.SendMessageAsync(message);
            }
            catch (Exception exception)
            {
                _log.WriteWarning(exception.Message);
            }
        }
    }
}
