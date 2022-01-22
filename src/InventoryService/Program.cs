using InventoryService;
using InventoryService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var bookService = new BookService();
var notificationProcessor = new NotificationProcessor(bookService);

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare(exchange: "book-event", type: ExchangeType.Fanout);
    var queueName = channel.QueueDeclare().QueueName;

    channel.QueueBind(queueName, "book-event", routingKey: "");

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var notification = JsonSerializer.Deserialize<Notification>(message);
        if (notification != null)
        {
            notificationProcessor.Process(notification);
        }

        Console.WriteLine($"Received {notification?.Event}");
    };

    channel.BasicConsume(queue: queueName,
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine("Press [enter] to exit.");
    Console.ReadLine();
}
