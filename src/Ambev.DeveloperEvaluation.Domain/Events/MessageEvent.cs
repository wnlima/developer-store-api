namespace Ambev.DeveloperEvaluation.Domain.Events;

public abstract class MessageEvent<T> where T : class
{
    public abstract string Source { get; }
    public abstract string EventVersion { get; }
    public abstract string EventName { get; }
    public DateTime EventDate { get; } = DateTime.UtcNow;
    public T Data { get; }
    public MessageEvent(T data)
    {
        Data = data;
    }
}