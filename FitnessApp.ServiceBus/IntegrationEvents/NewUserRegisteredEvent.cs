namespace FitnessApp.ServiceBus.IntegrationEvents
{
    public class NewUserRegisteredEvent
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
