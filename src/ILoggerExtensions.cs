using System;
using System.Runtime.CompilerServices;

namespace WB.Logging;

/// <summary>
/// Provides extension methods for <see cref="ILogger"/> to simplify common logging tasks.
/// </summary>
public static class ILoggerExtensions
{
    extension(ILogger @this)
    {
        /// <summary>
        /// Logs the <paramref name="exception"/>
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to log.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Exception(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception, nameof(exception));

            @this.Log(null, exception);
        }

        /// <summary>
        /// Logs the <paramref name="message"/> at <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <param name="message">The message to log.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info(object message)
            => @this.Log(LogLevel.Info, message);

        /// <summary>
        /// Logs the <paramref name="message"/> at <see cref="LogLevel.Warning"/>.
        /// </summary>
        /// <param name="message">The message to log.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(object message)
            => @this.Log(LogLevel.Warning, message);

        /// <summary>
        /// Logs the <paramref name="message"/> at <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="message">The message to log.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Common logging term.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(object message)
            => @this.Log(LogLevel.Error, message);
    }
}