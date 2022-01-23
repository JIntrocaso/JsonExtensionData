using PurchasingService.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PurchasingService
{
    public class NotificationService
    {
        public NotificationService()
        {

        }

        public void SendNotification(PublishMessage message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "book-event", type: "fanout");

                string jsonified = JsonSerializer.Serialize(message);

                var body = Encoding.UTF8.GetBytes(jsonified);
                channel.BasicPublish(exchange: "book-event",
                                        routingKey: "",
                                        basicProperties: null,
                                        body: body);
                Console.WriteLine(" [x] Sent {0}", message.Event);
            }
        }
    }
}
