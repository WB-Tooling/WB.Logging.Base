using System;

namespace WB.Logging;

internal sealed class LogMessageFilterWrapper<TPayload>(LogMessageFilter<TPayload> filter) 
    : ILogMessageFilterWrapper
    where TPayload : notnull
{
    private readonly LogMessageFilter<TPayload> filter = filter;

    public bool IsMatch(object logMessage)
        => filter((ILogMessage<TPayload>)logMessage);
}