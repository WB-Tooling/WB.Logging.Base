using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WB.Logging;

/// <summary>
/// A pipeline of <see cref="LogMessageFilter"/>s that can be used to filter <see cref="ILogMessage"/>s before they are processed by log sinks.
/// </summary>
public sealed class LogMessageFilterPipeline
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Private Fields                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘
    private readonly ConcurrentDictionary<LogMessageFilter, byte> logMessageFilters = new();

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Registers the <paramref name="filter"/> for <see cref="ILogMessage"/>.
    /// </summary>
    /// <param name="filter">The <see cref="LogMessageFilter"/> to register.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to unregister the filter.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is <c>null</c>.</exception>
    public IDisposable Add(LogMessageFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        logMessageFilters.TryAdd(filter, 0);

        return new DelegateDisposable(() =>
        {
            logMessageFilters.TryRemove(filter, out _);
        });
    }

    /// <summary>
    /// Determines whether the specified <paramref name="logMessage"/> matches the registered filter for
    /// its payload type.
    /// </summary>
    /// <param name="logMessage">The log message to check.</param>
    /// <returns><c>true</c> if the log message matches the filter; otherwise, <c>false</c>.</returns>
    public bool IsMatch(ILogMessage logMessage)
    {
        ArgumentNullException.ThrowIfNull(logMessage);

        foreach (LogMessageFilter logMessageFilter in logMessageFilters.Keys)
        {
            if (!logMessageFilter(logMessage))
            {
                return false;
            }
        }

        return true;
    }
}