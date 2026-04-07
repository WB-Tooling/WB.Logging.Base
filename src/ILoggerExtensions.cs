using System;
using System.Runtime.CompilerServices;

namespace WB.Logging;

/// <summary>
/// Provides extension methods for <see cref="ILogger"/> to simplify common logging tasks.
/// </summary>
public static class ILoggerExtensions
{
#pragma warning disable CA1034 // Nested types should not be visible
    extension(ILogger @this)
#pragma warning restore CA1034 // Nested types should not be visible
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
        /// Logs the <paramref name="payload"/> at <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <param name="payload">The payload to log.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info<TPayload>(TPayload payload)
            => @this.Log(LogLevel.Info, payload);

        /// <summary>
        /// Logs the <paramref name="payload"/> at <see cref="LogLevel.Warning"/>.
        /// </summary>
        /// <param name="payload">The payload to log.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning<TPayload>(TPayload payload)
            => @this.Log(LogLevel.Warning, payload);

        /// <summary>
        /// Logs the <paramref name="payload"/> at <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="payload">The payload to log.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Common logging term.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error<TPayload>(TPayload payload)
            => @this.Log(LogLevel.Error, payload);
    }
}