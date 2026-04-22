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
    /// Disables this log sink, preventing it from processing any log messages until it is re-enabled.
    /// </summary>
    /// <returns>A <see cref="IDisposable"/> that, when disposed, re-enables the log sink.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the log sink is already disabled.</exception>
    public IDisposable Disable();

    /// <summary>
    /// Submits a <see cref="ILogMessage{TPayload}"/> to this log sink for processing.
    /// </summary>
    /// <param name="logMessage">The <see cref="ILogMessage{TPayload}"/> to submit.</param>
    /// <typeparam name="TPayload">The type of the payload of the log message.</typeparam>
    public void Submit<TPayload>(ILogMessage<TPayload> logMessage)
        where TPayload : notnull;
}