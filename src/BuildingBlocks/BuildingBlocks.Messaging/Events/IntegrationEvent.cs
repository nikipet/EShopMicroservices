namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTimeOffset Timestamp => DateTimeOffset.UtcNow;
    public string? EventType => GetType().AssemblyQualifiedName;
}