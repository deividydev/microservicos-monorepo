using System.Reflection;
using Application.Order.Events;
using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Extensions;
using MessageBus.RabbitMQ.Subscriber;

var builder = WebApplication.CreateBuilder(args);

var applicationAssembly = Assembly.Load("Application");

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

builder.Services.AddRabbitMqMessaging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// result from events 
var subscriber = app.Services.GetRequiredService<RabbitMqSubscriber>();
subscriber.Subscribe(app.Services.GetRequiredService<IEventHandler<ApprovedPaymentEvent>>());
subscriber.Subscribe(app.Services.GetRequiredService<IEventHandler<PaymentRejectedEvent>>());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();