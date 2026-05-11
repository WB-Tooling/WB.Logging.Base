using System;
using AwesomeAssertions;
using WB.Logging;

namespace LogMessageFiltersTests.MethodTests.AddMethodTests;

public sealed class TheAddMethod
{
    [Test]
    public void ShouldThrowArgumentNullExceptionIfFilterIsNull()
    {
        // Arrange
        LogMessageFilters logMessageFilters = new();

        // Act
        Action act = () => logMessageFilters.Add(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>(because: "a null filter cannot be registered");
    }
}