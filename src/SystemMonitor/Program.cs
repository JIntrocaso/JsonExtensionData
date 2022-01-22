using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using SystemMonitor.Models;

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

        Console.WriteLine($"Received {notification?.Event}");
    };

    channel.BasicConsume(queue: queueName,
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine("Press [enter] to exit.");
    Console.ReadLine();
}