namespace WB.Logging;

/// <summary>
/// An abstract base class for log message filters.
/// </summary>
/// <typeparam name="TPayload">The type of the log message payload.</typeparam>
public abstract class LogMessageFilter<TPayload> : ILogMessageFilter<TPayload>
    where TPayload : notnull
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods.                                                             │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <inheritdoc/>
    public abstract bool IsMatch(ILogMessage<TPayload> logMessage);

    /// <inheritdoc/>
    public bool IsMatch(object logMessage)
        => logMessage is ILogMessage<TPayload> typed && IsMatch(typed);
}