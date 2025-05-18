using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MessageBus.RabbitMQ.Publisher;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchange = "exchange_saga";

    public RabbitMqPublisher(string connectionString)
    {
        var factory = new ConnectionFactory() { Uri = new Uri(connectionString) };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Fanout);
    }

    public void Publish<T>(T message)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        _channel.BasicPublish(exchange: _exchange, routingKey: "", basicProperties: null, body: body);
    }
}