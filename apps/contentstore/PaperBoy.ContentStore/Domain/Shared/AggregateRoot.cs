namespace PaperBoy.ContentStore.Domain.Shared;

/// <summary>
/// Represents the base class for aggregate roots in the domain.
/// </summary>
public abstract class AggregateRoot
{
    /// <summary>
    /// The list of pending domain events.
    /// </summary>
    private readonly List<object> _pendingDomainEvents = new();

    /// <summary>
    /// Gets or sets the version of the aggregate root.
    /// </summary>
    public long Version { get; protected set; }
    
    /// <summary>
    /// Emits a domain event and adds it to the list of pending domain events if it is successfully applied.
    /// </summary>
    /// <param name="domainEvent">The domain event to emit.</param>
    protected void EmitDomainEvent(object domainEvent)
    {
        if (TryApplyDomainEvent(domainEvent))
        {
            _pendingDomainEvents.Add(domainEvent);
        }
    }

    /// <summary>
    /// Tries to apply a domain event to the aggregate root.
    /// </summary>
    /// <param name="domainEvent">The domain event to apply.</param>
    /// <returns><c>true</c> if the domain event was successfully applied; otherwise, <c>false</c>.</returns>
    protected abstract bool TryApplyDomainEvent(object domainEvent);

    /// <summary>
    /// Gets the read-only collection of pending domain events.
    /// </summary>
    public IReadOnlyCollection<object> PendingDomainEvents => _pendingDomainEvents.AsReadOnly();

    /// <summary>
    /// Clears the list of pending domain events.
    /// </summary>
    public void ClearPendingDomainEvents()
    {
        _pendingDomainEvents.Clear();
    }
}