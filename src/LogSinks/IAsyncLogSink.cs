using System;
using System.Threading;
using System.Threading.Tasks;

namespace WB.Logging;

/// <summary>
/// A log sink that can receive and process log messages asynchronously.
/// </summary>
public interface IAsyncLogSink
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Submits a <see cref="ILogMessage{TPayload}"/> to this log sink for processing.
    /// </summary>
    /// <param name="logMessage">The <see cref="ILogMessage{TPayload}"/> to submit.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous operation of submitting the log message.</returns>
    public ValueTask WriteAsync<TPayload>(ILogMessage<TPayload> logMessage, CancellationToken cancellationToken)
        where TPayload : notnull;
}