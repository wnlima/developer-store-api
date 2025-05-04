using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Eventing;

public class UserRegisteredEvent : MessageEvent<GetUserResult>, INotification
{
    public override string Source => "Developer Store API";
    public override string EventName => "UserRegistered";
    public override string EventVersion => "1.0.0";
    public UserRegisteredEvent(GetUserResult data) : base(data)
    {
    }
}

public class SaleCreatedEvent : MessageEvent<SaleResult>, INotification
{
    public override string Source => "Developer Store API";
    public override string EventName => "SaleCreated";
    public override string EventVersion => "1.0.0";
    public SaleCreatedEvent(SaleResult data) : base(data)
    {
    }
}

public class SaleUpdatedEvent : MessageEvent<SaleResult>, INotification
{
    public override string Source => "Developer Store API";
    public override string EventName => "SaleUpdated";
    public override string EventVersion => "1.0.0";
    public SaleUpdatedEvent(SaleResult data) : base(data)
    {
    }
}

public class SaleCanceledEvent : MessageEvent<SaleResult>, INotification
{
    public override string Source => "Developer Store API";
    public override string EventName => "SaleCanceled";
    public override string EventVersion => "1.0.0";
    public SaleCanceledEvent(SaleResult data) : base(data)
    {
    }
}

public class SaleItemCreatedEvent : MessageEvent<SaleItemResult>, INotification
{
    public override string Source => "Developer Store API";
    public override string EventName => "SaleItemCreated";
    public override string EventVersion => "1.0.0";
    public SaleItemCreatedEvent(SaleItemResult data) : base(data)
    {
    }
}

public class SaleItemCanceledEvent : MessageEvent<SaleItemResult>, INotification
{
    public override string Source => "Developer Store API";
    public override string EventName => "SaleItemCanceled";
    public override string EventVersion => "1.0.0";
    public SaleItemCanceledEvent(SaleItemResult data) : base(data)
    {
    }
}