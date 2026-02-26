using Moq;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests.WarningMethodTests;

public sealed class TheWarningMethod
{
    [Test]
    public void ShouldLogAMessageWithTheWarningLogLevel()
    {
        // Arrange
        Mock<ILogger> loggerMock = new();

        // Act
        loggerMock.Object.Warning("Test");

        // Assert
        loggerMock.Verify(l => l.Log(
            LogLevel.Warning,
            It.IsAny<object>()),
        Times.Once);
    }
}