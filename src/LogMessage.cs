using System;
using System.Collections.Generic;

namespace WB.Logging;

/// <summary>
/// A log message.
/// </summary>
public readonly record struct LogMessage<TPayload>
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                           │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets the timestamp of the log message.
    /// </summary>
    public required DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Gets the senders of the log message.
    /// </summary>
    public required IReadOnlyList<string> Senders { get; init; }

    /// <summary>
    /// Gets the <see cref="LogLevel"/> of the log message.
    /// </summary>
    public LogLevel? LogLevel { get; init; }

    /// <summary>
    /// Gets the payload of the log message.
    /// </summary>
    public TPayload? Payload { get; init; }
}
