namespace WB.Logging;

public interface ILogMessageFilter
{
    public bool IsMatch<TPayload>(ILogMessage<TPayload> logMessage)
        where TPayload : notnull;
}