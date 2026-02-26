using Moq;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests.ErrorMethodTests;

public sealed class TheErrorMethod
{
    [Test]
    public void ShouldLogAMessageWithTheErrorLogLevel()
    {
        // Arrange
        Mock<ILogger> loggerMock = new();

        // Act
        loggerMock.Object.Error("Test");

        // Assert
        loggerMock.Verify(l => l.Log(
            LogLevel.Error,
            It.IsAny<object>()),
        Times.Once);
    }
}