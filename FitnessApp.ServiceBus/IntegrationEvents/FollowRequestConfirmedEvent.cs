namespace FitnessApp.ServiceBus.IntegrationEvents
{
    public class FollowRequestConfirmedEvent
    {
        public string UserId { get; set; }
        public string FollowerUserId { get; set; }
    }
}
