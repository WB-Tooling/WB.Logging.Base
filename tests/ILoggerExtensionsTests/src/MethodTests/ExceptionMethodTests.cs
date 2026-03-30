using System;
using FakeItEasy;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests.ExceptionMethodTests;

public sealed class TheExceptionMethod
{
    [Test]
    public void ShouldLogAMessageWithTheExceptionLogLevel()
    {
        // Arrange
        ILogger logger = A.Fake<ILogger>();
        InvalidOperationException exception = new("Test");

        // Act
        logger.Exception(exception);

        // Assert
        A.CallTo(() => logger.Log(
            null,
            exception))
        .MustHaveHappenedOnceExactly();
    }
}