using MessageBus.RabbitMQ.Publisher;
using MessageBus.RabbitMQ.Subscriber;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBus.RabbitMQ.Extensions;

/// <summary>
/// Provides extension methods for registering RabbitMQ services in the dependency injection container.
/// </summary>
public static class RabbitMqExtensions
{
    /// <summary>
    /// Registers the RabbitMQ publisher and subscriber implementations with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to which the RabbitMQ services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddRabbitMqMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IRabbitMqPublisher>(provider =>
        {
            var connectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING")
                                   ?? "amqp://guest:guest@localhost:5672";
            return new RabbitMqPublisher(connectionString);
        });

        services.AddSingleton<IRabbitMqSubscriber>(provider =>
        {
            var connectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING")
                                   ?? "amqp://guest:guest@localhost:5672";
            return new RabbitMqSubscriber(connectionString);
        });

        return services;
    }
}
