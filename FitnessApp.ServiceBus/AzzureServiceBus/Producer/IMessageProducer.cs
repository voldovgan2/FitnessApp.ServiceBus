using System.Threading.Tasks;

namespace FitnessApp.ServiceBus.AzzureServiceBus.Producer
{
    public interface IMessageProducer
    {
        Task SendMessage(string topic, string data);
    }
}
