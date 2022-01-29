using Azure.Messaging.ServiceBus;
using FitnessApp.Common.Logger;
using FitnessApp.ServiceBus.AzzureServiceBus.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace FitnessApp.ServiceBus.AzzureServiceBus.Consumer
{
    public class MessageConsumer : MessageBase, IMessageConsumer
    {
        private ServiceBusProcessor _processor;
        private Func<string, Task> _processMessage;
        public MessageConsumer
        (
            IOptions<AzzureServiceBusSettings> azzureServiceBusSettings,
            ILogger<MessageConsumer> log
        )
            : base(azzureServiceBusSettings, log)
        {
        }

        public async Task SubscribeMessage
        (
            string topic,
            string subscription,
            Func<string, Task> processMessage
        )
        {
            _processor = _serviceBusClient.CreateProcessor
            (
                topic,
                subscription,
                new ServiceBusProcessorOptions
                {
                    MaxConcurrentCalls = _azzureServiceBusSettings.MaxConcurrentCalls,
                    AutoCompleteMessages = _azzureServiceBusSettings.AutoCompleteMessages
                }
            );
            _processMessage = processMessage;
            _processor.ProcessMessageAsync += Processor_ProcessMessageAsync; ;
            _processor.ProcessErrorAsync += Processor_ProcessErrorAsync; ;
            await _processor.StartProcessingAsync();
        }

        public async Task UnsubscribeMessage()
        {
            await _processor.CloseAsync();
            _processMessage = null;
        }

        private async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            await _processMessage(arg.Message.Body.ToString());
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            _log.WriteWarning(arg.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
