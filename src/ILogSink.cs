namespace WB.Logging;

/// <summary>
/// A log sink that can receive and process log messages.
/// </summary>
public interface ILogSink
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Submits a <see cref="LogMessage"/> to this log sink for processing.
    /// </summary>
    /// <param name="logMessage">The <see cref="LogMessage"/> to submit.</param>
    public void Submit(LogMessage logMessage);
}