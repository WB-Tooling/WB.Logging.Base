using System;
using System.Threading;
using System.Threading.Tasks;

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
    public void Write<TPayload>(ILogMessage<TPayload> logMessage)
        where TPayload : notnull;
}