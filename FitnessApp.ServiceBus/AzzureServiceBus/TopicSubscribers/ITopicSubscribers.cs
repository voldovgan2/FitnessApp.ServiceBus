using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessApp.ServiceBus.AzzureServiceBus.TopicSubscribers
{
    public interface ITopicSubscribers
    {
        IEnumerable<Tuple<string, string, Func<string, Task>>> TopicsSubscribers { get; }
    }
}
