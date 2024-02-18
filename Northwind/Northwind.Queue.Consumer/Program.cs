using Queue.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using static System.Console;

string queueName = "product";

ConnectionFactory factory = new() { HostName = "localhost" };

using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

WriteLine("Declaring queue...");

QueueDeclareOk response = channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

WriteLine(
    "Queue name: {0}, Message count: {1}, Consumer count: {2}.",
    response.QueueName,
    response.MessageCount,
    response.ConsumerCount
);

WriteLine("Waiting for messages...");

EventingBasicConsumer consumer = new(channel);

consumer.Received += (model, args) =>
{
    byte[] body = args.Body.ToArray();

    ProductQueueMessage? message = JsonSerializer.Deserialize<ProductQueueMessage>(body);

    if (message is not null)
    {
        WriteLine(
            "Received product. Id: {0}, Name: {1}, Message: {2}",
            message.Product.ProductId,
            message.Product.ProductName,
            message.Text
        );
    }
    else
    {
        WriteLine("Received unknown: {0}.", args.Body.ToArray());
    }
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

WriteLine(">>> Press Enter to stop consuming and quit. <<<");
ReadLine();
