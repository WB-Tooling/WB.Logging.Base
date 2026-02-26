using System;
using Moq;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests.ExceptionMethodTests;

public sealed class TheExceptionMethod
{
    [Test]
    public void ShouldLogAMessageWithTheExceptionLogLevel()
    {
        // Arrange
        Mock<ILogger> loggerMock = new();
        InvalidOperationException exception = new("Test");

        // Act
        loggerMock.Object.Exception(exception);

        // Assert
        loggerMock.Verify(l => l.Log(
            null,
            exception),
        Times.Once);
    }
}