using System;

namespace WB.Logging;

/// <summary>
/// A filter for <see cref="ILogMessage{TPayload}"/>.
/// </summary>
/// <typeparam name="TPayload">The <see cref="Type"/> of the payload of the log message.</typeparam>
/// <param name="message">The log message to filter.</param>
/// <returns><c>true</c> if the log message matches the filter; otherwise, <c>false</c>.</returns>
public delegate bool LogMessageFilter<in TPayload>(ILogMessage<TPayload> message)
    where TPayload : notnull;