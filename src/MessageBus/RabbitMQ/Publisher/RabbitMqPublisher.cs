using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MessageBus.RabbitMQ.Publisher;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchange = "exchange_saga_direct";

    public RabbitMqPublisher(string connectionString)
    {
        var factory = new ConnectionFactory() { Uri = new Uri(connectionString) };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Direct);
    }

    public Task PublishAsync<T>(T message)
    {
        try
        {
            var routingKey = typeof(T).Name;
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: _exchange, routingKey: routingKey, basicProperties: null, body: body);
            Console.WriteLine($"[Publisher]  Publicando evento: {routingKey}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Publisher] Erro ao publicar mensagem: {ex}");
        }

        return Task.CompletedTask;
    }
}