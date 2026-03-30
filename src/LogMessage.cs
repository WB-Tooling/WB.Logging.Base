using System;
using System.Collections.Generic;

namespace WB.Logging;

/// <summary>
/// A log message.
/// </summary>
public readonly ref struct LogMessage
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                           │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets the timestamp of the log message.
    /// </summary>
    required public DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Gets the senders of the log message.
    /// </summary>
    required public IReadOnlyList<string> Senders { get; init; }

    /// <summary>
    /// Gets the <see cref="LogLevel"/> of the log message.
    /// </summary>
    public LogLevel? LogLevel { get; init; }

    /// <summary> 
    /// Gets the message object of the log message.
    /// </summary>
    public object? Message { get; init; }
}
