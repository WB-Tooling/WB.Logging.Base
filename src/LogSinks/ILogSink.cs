using System;

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
    /// Submits a <see cref="ILogMessage{TPayload}"/> to this log sink for processing.
    /// </summary>
    /// <param name="logMessage">The <see cref="ILogMessage{TPayload}"/> to submit.</param>
    /// <typeparam name="TPayload">The type of the payload of the log message.</typeparam>
    public void Submit<TPayload>(ILogMessage<TPayload> logMessage)
        where TPayload : notnull;
}