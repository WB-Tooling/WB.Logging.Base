using FakeItEasy;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests.InfoMethodTests;

public sealed class TheInfoMethod
{
    [Test]
    public void ShouldLogAMessageWithTheInfoLogLevel()
    {
        // Arrange
       ILogger logger = A.Fake<ILogger>();

        // Act
        logger.Info("Test");

        // Assert
        A.CallTo(() => logger.Log(
            LogLevel.Info,
            A<string>.Ignored))
        .MustHaveHappenedOnceExactly();
    }
}