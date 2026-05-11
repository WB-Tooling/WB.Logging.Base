using System;
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
    /// Submits a <see cref="LogMessage"/> to this log sink for processing.
    /// </summary>
    /// <param name="logMessage">The <see cref="LogMessage"/> to submit.</param>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous operation of submitting the log message.</returns>
    public ValueTask SubmitAsync(LogMessage logMessage);
}