namespace WB.Logging;

/// <summary>
/// A filter for log messages with payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <typeparam name="TPayload">The type of the log message payload.</typeparam>
public interface ILogMessageFilter<in TPayload> : ILogMessageFilter
    where TPayload : notnull
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Determines whether the specified <paramref name="logMessage"/> matches the filter criteria.
    /// </summary>
    /// <param name="logMessage">The log message to evaluate.</param>
    /// <returns><c>true</c> if the log message matches the filter criteria; otherwise, <c>false</c>.</returns>
    public bool IsMatch(ILogMessage<TPayload> logMessage);
}
