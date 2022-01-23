namespace PurchasingService.Models
{
    public class PublishMessage
    {
        public PublishMessage(string sender, string eventType, object? payload)
        {
            Sender = sender;
            Event = eventType;
            Payload = payload;
        }

        public Guid MessageId => Guid.NewGuid();
        public DateTime Timestamp => DateTime.UtcNow;
        public string Sender { get; init; } = string.Empty;
        public string Event { get; init; } = string.Empty;
        public object? Payload { get; init; }
    }
}
