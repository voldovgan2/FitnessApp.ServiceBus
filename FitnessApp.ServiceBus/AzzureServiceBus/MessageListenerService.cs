using FitnessApp.ServiceBus.AzzureServiceBus.Consumer;
using FitnessApp.ServiceBus.AzzureServiceBus.TopicSubscribers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessApp.AzzureServiceBus
{
    public class MessageListenerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly List<IMessageConsumer>  _serviceBusConsumers;
        private readonly ITopicSubscribers _topicsSubscribers;

        public MessageListenerService(IServiceProvider serviceProvider, ITopicSubscribers topicsSubscribers)
        {
            _serviceProvider = serviceProvider;
            _serviceBusConsumers = new List<IMessageConsumer>();
            _topicsSubscribers = topicsSubscribers;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var item in _topicsSubscribers.TopicsSubscribers)
            {
                var serviceBusConsumer = _serviceProvider.GetRequiredService<IMessageConsumer>();                
                await serviceBusConsumer.SubscribeMessage(item.Item1, item.Item2, item.Item3);
                _serviceBusConsumers.Add(serviceBusConsumer);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var item in _serviceBusConsumers)
            {
                await item.UnsubscribeMessage();
            }
        }
    }
}