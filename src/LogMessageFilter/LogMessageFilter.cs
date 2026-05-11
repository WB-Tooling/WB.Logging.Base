using System;

namespace WB.Logging;

/// <summary>
/// A filter for <see cref="LogMessage"/>.
/// </summary>
/// <param name="message">The log message to filter.</param>
/// <returns><c>true</c> if the log message matches the filter; otherwise, <c>false</c>.</returns>
public delegate bool LogMessageFilter(LogMessage message);
