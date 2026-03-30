using FakeItEasy;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests.WarningMethodTests;

public sealed class TheWarningMethod
{
    [Test]
    public void ShouldLogAMessageWithTheWarningLogLevel()
    {
        // Arrange
        ILogger logger = A.Fake<ILogger>();

        // Act
        logger.Warning("Test");

        // Assert
        A.CallTo(() => logger.Log(
            LogLevel.Warning,
            A<object>.Ignored))
        .MustHaveHappenedOnceExactly();
    }
}