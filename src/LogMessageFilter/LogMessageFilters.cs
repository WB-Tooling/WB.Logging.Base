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
    private readonly ConcurrentDictionary<Type, List<LogMessageFilter>> logMessageFilters = new();

    private readonly ConcurrentDictionary<Type, List<LogMessageFilter>?> logMessageFiltersCache = new();

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Registers the <paramref name="filter"/> for <see cref="LogMessage"/>.
    /// </summary>
    /// <param name="filter">The <see cref="LogMessageFilter"/> to register.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to unregister the filter.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is <c>null</c>.</exception>
    public IDisposable RegisterLogMessageFilter<TPayload>(LogMessageFilter filter)
        where TPayload : notnull
    {
        ArgumentNullException.ThrowIfNull(filter);

        List<LogMessageFilter> list = logMessageFilters.GetOrAdd(typeof(TPayload), _ => new List<LogMessageFilter>(capacity: 2));

        lock (list)
        {
            list.Add(filter);
        }

        logMessageFiltersCache.Clear();

        return new DelegateDisposable(() =>
        {
            if (logMessageFilters.TryGetValue(typeof(TPayload), out List<LogMessageFilter>? list))
            {
                lock (list)
                {
                    list.Remove(filter);

                    if (list.Count == 0)
                    {
                        logMessageFilters.TryRemove(typeof(TPayload), out _);
                    }
                }
            }
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

        List<LogMessageFilter>? filters = GetLogMessageFilter(logMessage.Payload.GetType());

        if (filters is null)
        {
            return true;
        }
        else
        {
            foreach (LogMessageFilter filter in filters)
            {
                if (!filter(logMessage))
                {
                    return false;
                }
            }
        }

        return true;
    }

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Private Methods                                                             │
    // └─────────────────────────────────────────────────────────────────────────────┘
    private List<LogMessageFilter>? GetLogMessageFilter(Type payloadType)
    {
        if (logMessageFiltersCache.TryGetValue(payloadType, out List<LogMessageFilter>? cached))
        {
            return cached;
        }

        List<LogMessageFilter> result = new(capacity: 4);

        if (logMessageFilters.TryGetValue(payloadType, out List<LogMessageFilter>? exact))
        {
            result.AddRange(exact);
        }

        Type? baseType = payloadType.BaseType;

        while (baseType is not null)
        {
            if (logMessageFilters.TryGetValue(baseType, out List<LogMessageFilter>? baseFilter))
            {
                result.AddRange(baseFilter);
            }

            baseType = baseType.BaseType;
        }

        foreach (Type? @interface in payloadType.GetInterfaces())
        {
            if (logMessageFilters.TryGetValue(@interface, out List<LogMessageFilter>? interfaceFilter))
            {
                result.AddRange(interfaceFilter);
            }
        }

        if (result.Count == 0)
        {
            return logMessageFiltersCache[payloadType] = null;
        }
        else
        {
            return logMessageFiltersCache[payloadType] = result;
        }
    }
}