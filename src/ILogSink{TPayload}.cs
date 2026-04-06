namespace WB.Logging;

/// <summary>
/// A log sink that can receive and process log messages with payload
/// of type <typeparamref name="TPayload"/>.
/// </summary>
/// <typeparam name="TPayload">The type of the payload of the log messages that this log sink can process.</typeparam>
public interface ILogSink<TPayload>
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Submits a <see cref="ILogMessage{TPayload}"/> to this log sink for processing.
    /// </summary>
    /// <param name="logMessage">The <see cref="ILogMessage{TPayload}"/> to submit.</param>
    public void Submit(ILogMessage<TPayload> logMessage);
}
