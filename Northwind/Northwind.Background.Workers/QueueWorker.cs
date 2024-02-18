using Queue.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Background.Workers;

public class QueueWorker : BackgroundService
{
    private readonly ILogger<QueueWorker> _logger;

    private const string queueNameAndRoutingKey = "product";
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly EventingBasicConsumer _consumer;

    public QueueWorker(ILogger<QueueWorker> logger)
    {
        _logger = logger;

        _factory = new() { HostName = "localhost" };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _consumer = new(_channel);

        _channel.QueueDeclare(
            queue: queueNameAndRoutingKey,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        _consumer = new(_channel);

        _consumer.Received += (model, args) =>
        {
            byte[] body = args.Body.ToArray();

            ProductQueueMessage? message = JsonSerializer.Deserialize<ProductQueueMessage>(body);

            if (message is not null)
            {
                _logger.LogInformation(
                    $"Received product. Id: {
          message.Product.ProductId}, Name: {message.Product
          .ProductName}, Message: {message.Text}"
                );
            }
            else
            {
                _logger.LogInformation($"Received unknown: {args.Body.ToArray()}.");
            }
        };

        _channel.BasicConsume(queue: queueNameAndRoutingKey, autoAck: true, consumer: _consumer);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
