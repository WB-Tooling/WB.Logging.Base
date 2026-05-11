using System;
using System.Collections.Generic;

namespace WB.Logging;

/// <summary>
/// A log message.
/// </summary>
public interface ILogMessage
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                           │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets the timestamp of the log message.
    /// </summary>
    public DateTimeOffset Timestamp { get; }

    /// <summary>
    /// Gets the senders of the log message.
    /// </summary>
    public IReadOnlyList<string> Senders { get; }

    /// <summary>
    /// Gets the <see cref="LogLevel"/> of the log message.
    /// </summary>
    public LogLevel? LogLevel { get; }

    /// <summary>
    /// Gets the payload of the log message.
    /// </summary>
    public object Payload { get; }
}
