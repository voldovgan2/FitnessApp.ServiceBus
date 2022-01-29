namespace FitnessApp.ServiceBus.Messages
{
    public class NewUserRegistered
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
