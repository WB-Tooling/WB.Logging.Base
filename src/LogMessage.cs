using System;
using System.Collections.Generic;

namespace WB.Logging;

/// <summary>
/// A log message.
/// </summary>
public sealed record LogMessage
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                           │
    // └─────────────────────────────────────────────────────────────────────────────┘
    private readonly List<string> senders = [];

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                           │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets the timestamp of the log message.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.Now;

    /// <summary>
    /// Gets the senders of the log message.
    /// </summary>
    public IReadOnlyList<string> Senders => senders.AsReadOnly();

    /// <summary>
    /// Gets the <see cref="LogLevel"/> of the log message.
    /// </summary>
    public LogLevel? LogLevel { get; init; }

    /// <summary>
    /// Gets the payload of the log message.
    /// </summary>
    public required object Payload { get; init; }

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Internal Properties                                                         │
    // └─────────────────────────────────────────────────────────────────────────────┘
    internal void AddSender(string sender)
        => senders.Add(sender);

}