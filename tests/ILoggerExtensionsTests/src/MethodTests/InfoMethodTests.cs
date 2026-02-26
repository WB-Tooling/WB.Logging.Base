using Moq;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests.InfoMethodTests;

public sealed class TheInfoMethod
{
    [Test]
    public void ShouldLogAMessageWithTheInfoLogLevel()
    {
        // Arrange
        Mock<ILogger> loggerMock = new();

        // Act
        loggerMock.Object.Info("Test");

        // Assert
        loggerMock.Verify(l => l.Log(
            LogLevel.Info,
            It.IsAny<object>()),
        Times.Once);
    }
}