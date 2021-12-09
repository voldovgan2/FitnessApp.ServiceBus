namespace FitnessApp.ServiceBus.IntegrationEvents
{
    public class NewFollowRequestEvent
    {
        public string UserId { get; set; }
        public string UserToFollowId { get; set; }
    }
}
