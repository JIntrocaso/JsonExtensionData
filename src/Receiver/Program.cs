using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Receiver.Models;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{

    channel.QueueDeclare(queue: "basic-queue",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var notification = JsonSerializer.Deserialize<Notification>(message);

        Console.WriteLine($"Received {notification.Event}");
    };

    channel.BasicConsume(queue: "basic-queue",
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine("Press [enter] to exit.");
    Console.ReadLine();
}