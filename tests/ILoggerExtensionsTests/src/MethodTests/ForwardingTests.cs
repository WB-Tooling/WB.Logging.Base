// ┌────────────────────────────────────────────────────────────────────────────────┐
// │ File: ForwardingTests.cs                                                       │
// │ Author: GitHub Copilot                                                         │
// │ Created: 2026-11-12                                                            │
// └────────────────────────────────────────────────────────────────────────────────┘
using System;
using AwesomeAssertions;
using FakeItEasy;
using WB.Logging;

namespace ILoggerExtensionsTests.MethodTests;

/// <summary>Checks that logging extensions forward their arguments.</summary>
public sealed class ForwardingTests
{
    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>Checks the debug level and payload.</summary>
    [Test]
    public void Debug_ForwardsPayloadAndLevel()
    {
        ILogger logger = A.Fake<ILogger>();

        logger.Debug(42);

        A.CallTo(() => logger.Log(LogLevel.Debug, 42)).MustHaveHappenedOnceExactly();
    }

    /// <summary>Checks the info level and payload.</summary>
    [Test]
    public void Info_ForwardsPayloadAndLevel()
    {
        ILogger logger = A.Fake<ILogger>();

        logger.Info(42);

        A.CallTo(() => logger.Log(LogLevel.Info, 42)).MustHaveHappenedOnceExactly();
    }

    /// <summary>Checks the warning level and payload.</summary>
    [Test]
    public void Warning_ForwardsPayloadAndLevel()
    {
        ILogger logger = A.Fake<ILogger>();

        logger.Warning(42);

        A.CallTo(() => logger.Log(LogLevel.Warning, 42)).MustHaveHappenedOnceExactly();
    }

    /// <summary>Checks the error level and payload.</summary>
    [Test]
    public void Error_ForwardsPayloadAndLevel()
    {
        ILogger logger = A.Fake<ILogger>();

        logger.Error(42);

        A.CallTo(() => logger.Log(LogLevel.Error, 42)).MustHaveHappenedOnceExactly();
    }

    /// <summary>Checks that the original exception is forwarded without a level.</summary>
    [Test]
    public void Exception_ForwardsOriginalExceptionWithoutLevel()
    {
        ILogger logger = A.Fake<ILogger>();
        InvalidOperationException exception = new("failure");

        logger.Exception(exception);

        A.CallTo(() => logger.Log(null, A<Exception>.That.IsSameAs(exception)))
            .MustHaveHappenedOnceExactly();
    }

    /// <summary>Checks that null is rejected before the logger is called.</summary>
    [Test]
    public void Exception_Null_ThrowsWithoutLogging()
    {
        ILogger logger = A.Fake<ILogger>();
        Action act = () => logger.Exception(null!);

        act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("exception");
        A.CallTo(() => logger.Log(A<LogLevel?>._, A<Exception>._)).MustNotHaveHappened();
    }
}
