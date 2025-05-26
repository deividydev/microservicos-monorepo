using System.Reflection;
using Application.Order.EventHandlers;
using Application.Order.Events;
using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Extensions;
using MessageBus.RabbitMQ.Subscriber;
using Presentation.Helpers;

var builder = WebApplication.CreateBuilder(args);

var applicationAssembly = Assembly.Load("Application");

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

builder.Services.AddSingleton<IEventHandler<ApprovedPaymentEvent>, ApprovedPaymentEventHandler>();
builder.Services.AddSingleton<IEventHandler<PaymentRejectedEvent>, PaymentRejectedEventHandler>();

builder.Services.AddRabbitMqMessaging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.Services.SubscribeEvents();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();