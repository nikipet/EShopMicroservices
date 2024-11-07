using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => new Guid();
    public DateTimeOffset Timestamp => DateTimeOffset.UtcNow;
    public string? EventType => GetType().AssemblyQualifiedName;
}