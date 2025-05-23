using System.Text;
using System.Text.Json;
using MessageBus.RabbitMQ.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBus.RabbitMQ.Subscriber;

public class RabbitMqSubscriber : IRabbitMqSubscriber
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchange = "exchange_saga";

    public RabbitMqSubscriber(string rabbitMqConnectionString)
    {
        var factory = new ConnectionFactory() { Uri = new Uri(rabbitMqConnectionString) };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Fanout);
    }

    public void Subscribe<T>(IEventHandler<T> handler)
    {
        var queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: queueName, exchange: _exchange, routingKey: "");

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));

            if (message != null)
                await handler.HandleAsync(message);

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }
}
