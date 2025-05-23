using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Extensions;
using MessageBus.RabbitMQ.Subscriber;
using Worker;
using Worker.EventHandlers;
using Worker.Events;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddRabbitMqMessaging();

        services.AddSingleton<RabbitMqSubscriber>();

        services.AddTransient<IEventHandler<CreatedOrderEvent>, CreatedOrderEventHandler>();

        services.AddHostedService<PaymentWorker>();
    })
    .Build();

// results events
var subscriber = host.Services.GetRequiredService<RabbitMqSubscriber>();
subscriber.Subscribe(host.Services.GetRequiredService<IEventHandler<CreatedOrderEvent>>());

// Executar
await host.RunAsync();