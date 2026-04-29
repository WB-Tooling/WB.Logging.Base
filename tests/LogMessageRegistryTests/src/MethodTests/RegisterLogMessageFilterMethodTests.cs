using System;
using AwesomeAssertions;
using WB.Logging;

namespace LogMessageRegistryTests.MethodTests.RegisterLogMessageFilterMethodTests;

public sealed class TheRegisterLogMessageFilterMethod
{
    [Test]
    public void ShouldThrowArgumentNullExceptionIfFilterIsNull()
    {
        // Arrange
        LogMessageFilterRegistry registry = new();

        // Act
        Action act = () => registry.RegisterLogMessageFilter<string>(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>(because: "a null filter cannot be registered");
    }
}