using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WB.Logging;

/// <summary>
/// A set of <see cref="LogMessageFilter"/>s registered for different payload types.
/// </summary>
public sealed class LogMessageFilters
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Private Fields                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘
    private readonly ConcurrentBag<LogMessageFilter> logMessageFilters = [];

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Registers the <paramref name="filter"/> for <see cref="LogMessage"/>.
    /// </summary>
    /// <param name="filter">The <see cref="LogMessageFilter"/> to register.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to unregister the filter.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is <c>null</c>.</exception>
    public IDisposable Add(LogMessageFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        logMessageFilters.Add(filter);

        return new DelegateDisposable(() =>
        {
            logMessageFilters.TryTake(out LogMessageFilter? _);
        });
    }

    /// <summary>
    /// Determines whether the specified <paramref name="logMessage"/> matches the registered filter for
    /// its payload type.
    /// </summary>
    /// <param name="logMessage">The log message to check.</param>
    /// <returns><c>true</c> if the log message matches the filter; otherwise, <c>false</c>.</returns>
    public bool IsMatch(LogMessage logMessage)
    {
        ArgumentNullException.ThrowIfNull(logMessage);

        foreach (LogMessageFilter logMessageFilter in logMessageFilters)
        {
            if (!logMessageFilter(logMessage))
            {
                return false;
            }
        }

        return true;
    }
}