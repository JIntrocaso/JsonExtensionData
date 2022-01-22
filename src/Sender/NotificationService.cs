using RabbitMQ.Client;
using Sender.Models;
using System.Text;
using System.Text.Json;

namespace Sender
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
                channel.QueueDeclare(queue: "basic-queue",
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);
                string jsonified = JsonSerializer.Serialize(message);

                var body = Encoding.UTF8.GetBytes(jsonified);
                channel.BasicPublish(exchange: "",
                                        routingKey: "basic-queue",
                                        basicProperties: null,
                                        body: body);
                Console.WriteLine(" [x] Sent {0}", message.Event);
            }
        }
    }
}
