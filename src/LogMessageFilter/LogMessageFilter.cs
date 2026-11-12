namespace WB.Logging;

/// <summary>
/// A filter for <see cref="ILogMessage"/>s.
/// </summary>
/// <remarks>
/// A <see cref="LogMessageFilter"/> is a delegate that takes an <see cref="ILogMessage"/> and returns a <see cref="bool"/> indicating whether the log message matches the filter criteria. It can be used to filter log messages before they are processed by log sinks.
/// </remarks>
/// <param name="message">The log message to filter.</param>
/// <returns><c>true</c> if the log message matches the filter; otherwise, <c>false</c>.</returns>
public delegate bool LogMessageFilter(ILogMessage message);
