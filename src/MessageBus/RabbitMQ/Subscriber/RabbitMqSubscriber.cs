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

        _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Direct);
    }

    public void Subscribe<T>(IEventHandler<T> handler)
    {
        try
        {
            var queueName = $"queue_{typeof(T).Name.ToLower()}";
            var routingKey = typeof(T).Name;

            _channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            _channel.QueueBind(queue: queueName, exchange: _exchange, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    var @event = JsonSerializer.Deserialize<T>(json);

                    if (@event != null)
                        await handler.HandleAsync(@event);

                    _channel.BasicAck(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Subscriber] Erro no handler: {ex}");
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Subscriber] Erro ao processar mensagem: {ex}");
        }
    }
}
