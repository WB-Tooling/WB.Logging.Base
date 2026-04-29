namespace WB.Logging;

internal interface ILogMessageFilterWrapper
{
    /// <summary>
    /// Determines whether the specified <paramref name="logMessage"/> matches the filter.
    /// </summary>
    /// <param name="logMessage">The log message to check.</param>
    /// <returns><c>true</c> if the log message matches the filter; otherwise, <c>false</c>.</returns>
    bool IsMatch(object logMessage);
}