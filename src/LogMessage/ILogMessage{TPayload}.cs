using System;
using System.Collections.Generic;

namespace WB.Logging;

/// <summary>
/// A log message with a strongly-typed <see cref="Payload"/>.
/// </summary>
/// <typeparam name="TPayload">The <see cref="Type"/> of the payload.</typeparam>
public interface ILogMessage<TPayload> : ILogMessage
    where TPayload : notnull
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                           │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets the payload of the log message.
    /// </summary>
    public new TPayload Payload { get; }
}
