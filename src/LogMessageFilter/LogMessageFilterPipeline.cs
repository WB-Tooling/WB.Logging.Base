using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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

    private volatile LogMessageFilter? compiledFilters;

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

        compiledFilters = Recompile();

        return new DelegateDisposable(() =>
        {
            logMessageFilters.TryRemove(filter, out _);

            compiledFilters = Recompile();
        });
    }

    /// <summary>
    /// Determines whether the specified <paramref name="logMessage"/> matches the registered filter for
    /// its payload type.
    /// </summary>
    /// <param name="logMessage">The log message to check.</param>
    /// <returns><c>true</c> if the log message matches the filter; otherwise, <c>false</c>.</returns>
    public bool IsMatch(ILogMessage logMessage)
        => compiledFilters?.Invoke(logMessage) ?? true;

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Private Methods                                                             │
    // └─────────────────────────────────────────────────────────────────────────────┘
    private LogMessageFilter? Recompile()
    {
        // Snapshot
        LogMessageFilter[] logMessageFilters = [.. this.logMessageFilters.Keys];

        if (logMessageFilters.Length == 0)
        {
            return null;
        }

        return logMessage =>
        {
            for (int i = 0; i < logMessageFilters.Length; i++)
            {
                if (!logMessageFilters[i](logMessage))
                {
                    return false;
                }
            }
    
            return true;
        };
    }
}