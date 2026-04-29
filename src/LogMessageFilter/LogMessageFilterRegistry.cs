using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WB.Logging;

/// <summary>
/// Manages registration and matching of log message filters for different payload types.
/// </summary>
public sealed class LogMessageFilterRegistry
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Private Fields                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘
    private readonly ConcurrentDictionary<Type, List<ILogMessageFilterWrapper>> logMessageFilters = new();

    private readonly ConcurrentDictionary<Type, List<ILogMessageFilterWrapper>?> logMessageFiltersCache = new();

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Registers the <paramref name="filter"/> for <see cref="ILogMessage{TPayload}"/> with payload type <typeparamref name="TPayload"/>.
    /// </summary>
    /// <param name="filter">The <see cref="LogMessageFilter{TPayload}"/> to register.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to unregister the filter.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is <c>null</c>.</exception>
    public IDisposable RegisterLogMessageFilter<TPayload>(LogMessageFilter<TPayload> filter)
        where TPayload : notnull
    {
        ArgumentNullException.ThrowIfNull(filter);

        List<ILogMessageFilterWrapper> list = logMessageFilters.GetOrAdd(typeof(TPayload), _ => new List<ILogMessageFilterWrapper>(capacity: 2));

        LogMessageFilterWrapper<TPayload> wrapper = new(filter);

        lock (list)
        {
            list.Add(wrapper);
        }

        logMessageFiltersCache.Clear();

        return new DelegateDisposable(() =>
        {
            if (logMessageFilters.TryGetValue(typeof(TPayload), out List<ILogMessageFilterWrapper>? list))
            {
                lock (list)
                {
                    list.Remove(wrapper);

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
    /// its payload <typeparamref name="TPayload"/>.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    /// <param name="logMessage">The log message to check.</param>
    /// <returns><c>true</c> if the log message matches the filter; otherwise, <c>false</c>.</returns>
    public bool IsMatch<TPayload>(ILogMessage<TPayload> logMessage)
        where TPayload : notnull
    {
        List<ILogMessageFilterWrapper>? filter = GetLogMessageFilter<TPayload>();

        if (filter is null)
        {
            return true;
        }
        else
        {
            foreach (ILogMessageFilterWrapper wrapper in filter)
            {
                if (!wrapper.IsMatch(logMessage))
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
    private List<ILogMessageFilterWrapper>? GetLogMessageFilter<TPayload>()
        where TPayload : notnull
    {
        Type type = typeof(TPayload);

        if (logMessageFiltersCache.TryGetValue(type, out List<ILogMessageFilterWrapper>? cached))
        {
            return cached;
        }

        List<ILogMessageFilterWrapper> result = new(capacity: 4);

        if (logMessageFilters.TryGetValue(type, out List<ILogMessageFilterWrapper>? exact))
        {
            result.AddRange(exact);
        }

        Type? baseType = type.BaseType;

        while (baseType is not null)
        {
            if (logMessageFilters.TryGetValue(baseType, out List<ILogMessageFilterWrapper>? baseFilter))
            {
                result.AddRange(baseFilter);
            }

            baseType = baseType.BaseType;
        }

        foreach (Type? @interface in type.GetInterfaces())
        {
            if (logMessageFilters.TryGetValue(@interface, out List<ILogMessageFilterWrapper>? interfaceFilter))
            {
                result.AddRange(interfaceFilter);
            }
        }

        if (result.Count == 0)
        {
            return logMessageFiltersCache[type] = null;
        }
        else
        {
            return logMessageFiltersCache[type] = result;
        }
    }
}