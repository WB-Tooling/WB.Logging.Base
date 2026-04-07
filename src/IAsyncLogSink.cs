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
    /// <typeparam name="TPayload">The type of the payload of the log message.</typeparam>
    /// <returns>A <see cref="ValueTask"/> that represents the asynchronous operation of submitting the log message.</returns>
    public ValueTask SubmitAsync<TPayload>(ILogMessage<TPayload> logMessage);
}