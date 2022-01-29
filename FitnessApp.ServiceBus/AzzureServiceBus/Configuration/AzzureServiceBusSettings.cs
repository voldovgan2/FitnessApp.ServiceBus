namespace FitnessApp.ServiceBus.AzzureServiceBus.Configuration
{
    public class AzzureServiceBusSettings
    {
        public string ConnectionString { get; set; }
        public int MaxConcurrentCalls { get; set; }
        public bool AutoCompleteMessages { get; set; }
    }
}
