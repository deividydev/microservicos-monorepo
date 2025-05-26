using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Extensions;
using Worker;
using Worker.EventHandlers;
using Worker.Events;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddRabbitMqMessaging();
        services.AddTransient<IEventHandler<CreatedOrderEvent>, CreatedOrderEventHandler>();
        services.AddHostedService<PaymentWorker>();
    })
    .Build();

await host.RunAsync();