namespace Ordering.Domain.Abstractions;

public interface IEntity<T> : IEntity
{
    public T Id { get; }
}
public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}