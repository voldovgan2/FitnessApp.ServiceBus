using NATS.Client;
using System;

namespace FitnessApp.AzzureServiceBus
{
    public interface IServiceBus
    {
        void PublishEvent(string subject, byte[] data);
        IAsyncSubscription SubscribeEvent(string subject, EventHandler<MsgHandlerEventArgs> handler);
        void UnsubscribeEvent(IAsyncSubscription subscription);
    }
}
