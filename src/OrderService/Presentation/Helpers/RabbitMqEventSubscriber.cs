using Application.Order.Events;
using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Subscriber;

namespace Presentation.Helpers;

/// <summary>
/// Helper class to centralize the subscription of RabbitMQ event handlers.
/// </summary>
public static class RabbitMqEventSubscriber
{
    /// <summary>
    /// Subscribes all necessary event handlers to the RabbitMQ subscriber.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve dependencies.</param>
    public static void SubscribeEvents(this IServiceProvider serviceProvider)
    {
        var subscriber = serviceProvider.GetRequiredService<IRabbitMqSubscriber>();

        subscriber.Subscribe(serviceProvider.GetRequiredService<IEventHandler<ApprovedPaymentEvent>>());
        subscriber.Subscribe(serviceProvider.GetRequiredService<IEventHandler<PaymentRejectedEvent>>());
    }
}
