using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Eventing;

public class EventPublisherHandler :
            INotificationHandler<SaleCreatedEvent>,
            INotificationHandler<SaleUpdatedEvent>,
            INotificationHandler<SaleCanceledEvent>,
            INotificationHandler<SaleItemCreatedEvent>,
            INotificationHandler<UserRegisteredEvent>,
            INotificationHandler<SaleItemCanceledEvent>
{
    private readonly ILogger _logger;

    public EventPublisherHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(notification, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        _logger.LogInformation($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        _logger.LogInformation(json);

        Console.WriteLine($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        Console.WriteLine(json);

        return Task.CompletedTask;
    }

    public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(notification, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        _logger.LogInformation($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        _logger.LogInformation(json);

        Console.WriteLine($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        Console.WriteLine(json);

        return Task.CompletedTask;
    }

    public Task Handle(SaleUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(notification, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        _logger.LogInformation($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        _logger.LogInformation(json);

        Console.WriteLine($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        Console.WriteLine(json);

        return Task.CompletedTask;
    }

    public Task Handle(SaleCanceledEvent notification, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(notification, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        _logger.LogInformation($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        _logger.LogInformation(json);

        Console.WriteLine($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        Console.WriteLine(json);

        return Task.CompletedTask;
    }

    public Task Handle(SaleItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(notification, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        _logger.LogInformation($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        _logger.LogInformation(json);

        Console.WriteLine($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        Console.WriteLine(json);

        return Task.CompletedTask;
    }

    public Task Handle(SaleItemCanceledEvent notification, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(notification, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        _logger.LogInformation($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        _logger.LogInformation(json);

        Console.WriteLine($"[EVENT] {notification.EventName} triggered at {DateTime.UtcNow}");
        Console.WriteLine(json);

        return Task.CompletedTask;
    }
}
