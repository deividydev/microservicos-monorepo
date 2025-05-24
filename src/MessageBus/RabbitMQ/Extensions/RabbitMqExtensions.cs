using MessageBus.RabbitMQ.Publisher;
using MessageBus.RabbitMQ.Subscriber;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBus.RabbitMQ.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMqMessaging(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING")
                               ?? "amqp://guest:guest@localhost:5672";

        services.AddSingleton(new RabbitMqPublisher(connectionString));
        services.AddSingleton(new RabbitMqSubscriber(connectionString));

        return services;
    }
}
