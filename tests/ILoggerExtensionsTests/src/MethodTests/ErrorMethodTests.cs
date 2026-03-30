using FakeItEasy;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests.ErrorMethodTests;

public sealed class TheErrorMethod
{
    [Test]
    public void ShouldLogAMessageWithTheErrorLogLevel()
    {
        // Arrange
        ILogger logger = A.Fake<ILogger>();

        // Act
        logger.Error("Test");

        // Assert
        A.CallTo(() => logger.Log(
            LogLevel.Error,
            A<object>.Ignored))
        .MustHaveHappenedOnceExactly();
    }
}