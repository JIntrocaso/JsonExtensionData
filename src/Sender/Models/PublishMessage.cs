namespace Sender.Models
{
    public class PublishMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; }
        public string Service { get; set; } = "DataService";
        public string Event { get; set; } = string.Empty;
        public string SenderName { get; set; } = "Jamie";

        public object Payload { get; set; }
    }
}
