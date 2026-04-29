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
    /// Gets the name of the logger.
    /// </summary>
    public string Name { get; }

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

    /// <summary>
    /// Gets the minimum <see cref="LogLevel"/> for <see cref="ILogMessage{TPayload}"/>s.
    /// </summary>
    public LogLevel? MinimumLogLevel { get; set; }

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods.                                                             │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Logs a <paramref name="payload"/> at the specified <paramref name="logLevel"/>.
    /// </summary>
    /// <param name="logLevel">The <see cref="LogLevel"/>.</param>
    /// <param name="payload">The payload to log.</param>
    public void Log<TPayload>(LogLevel? logLevel, TPayload payload)
        where TPayload : notnull;

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

    /// <summary>
    /// Attaches a <see cref="IAsyncLogSink"/> to this logger. The log sink will receive all log messages submitted to this logger.
    /// </summary>
    /// <param name="logSink">The <see cref="IAsyncLogSink"/> to attach.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to detach the log sink.</returns>
    public IDisposable AttachLogSink(IAsyncLogSink logSink);

    /// <summary>
    /// Creates and returns a child logger with the specified <paramref name="name"/>. 
    /// The child <see cref="ILogger"/> will have this <see cref="ILogger"/> as its <see cref="Parent"/>.
    /// </summary>
    /// <param name="name">The name of the child logger.</param>
    /// <returns>The created child <see cref="ILogger"/>.</returns>
    public ILogger CreateChildLogger(string name);

    /// <summary>
    /// Attaches a <see cref="LogMessageFilter{TPayload}"/> to this logger. The filter will be applied to all log messages of type <typeparamref name="TPayload"/> submitted to this logger.
    /// </summary>
    /// <param name="filter">The <see cref="LogMessageFilter{TPayload}"/> to attach.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to detach the filter.</returns>
    public IDisposable AddLogMessageFilter<TPayload>(LogMessageFilter<TPayload> filter)
        where TPayload : notnull;
}
