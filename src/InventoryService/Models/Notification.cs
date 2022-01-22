using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public class Notification
    {
        public Guid MessageId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Sender { get; set; } = "None Sent";
        public string Event { get; set; } = string.Empty;

        [JsonExtensionData]
        public Dictionary<string, object>? Properties { get; set; }
    }
}
