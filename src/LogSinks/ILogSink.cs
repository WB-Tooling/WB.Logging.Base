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
    /// Adds the <paramref name="filter"/> to this log sink. The filter will be used 
    /// to determine whether a log message should be processed by this log sink or not.
    /// </summary>
    /// <param name="filter">The filter to add.</param>
    /// <returns>A <see cref="IDisposable"/> that, when disposed, removes the filter from the log sink.</returns>
    public IDisposable AddFilter(ILogMessageFilter filter);

    /// <summary>
    /// Submits a <see cref="ILogMessage{TPayload}"/> to this log sink for processing.
    /// </summary>
    /// <param name="logMessage">The <see cref="ILogMessage{TPayload}"/> to submit.</param>
    /// <typeparam name="TPayload">The type of the payload of the log message.</typeparam>
    public void Submit<TPayload>(ILogMessage<TPayload> logMessage)
        where TPayload : notnull;
}