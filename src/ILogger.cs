using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WB.Logging;

/// <summary>
/// A logger.
/// </summary>
public interface ILogger : IAsyncDisposable
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties.                                                          │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets the parent <see cref="ILogger"/>.
    /// </summary>
    /// <remarks>
    /// If this logger has a parent logger, log messages submitted to this logger will also be submitted to the parent logger.
    /// This allows for hierarchical logging, where log messages can be propagated up a chain of log
    /// loggers, each of which can have its own log sinks and log level filters.
    /// </remarks>
    public ILogger? Parent { get; }

    /// <summary>
    /// Gets the list of attached <see cref="ILogSink"/>s.
    /// </summary>
    /// <remarks>
    /// The log sinks in this list will receive all log messages submitted to this logger.
    /// </remarks>
    public IReadOnlyList<ILogSink> LogSinks { get; }

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods.                                                             │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Logs a <paramref name="message"/> at the specified <paramref name="logLevel"/>.
    /// </summary>
    /// <param name="logLevel">The <see cref="LogLevel"/>.</param>
    /// <param name="message">The message to log.</param>
    public void Log(LogLevel? logLevel, object message);

    /// <summary>
    /// Flushes all pending log messages.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the flush to complete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous flush operation.</returns>
    public Task FlushAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Attaches a <see cref="ILogSink"/> to this logger. The log sink will receive all log messages submitted to this logger.
    /// </summary>
    /// <param name="logSink">The <see cref="ILogSink"/> to attach.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to detach the log sink.</returns>
    public IDisposable AttachLogSink(ILogSink logSink);
}
