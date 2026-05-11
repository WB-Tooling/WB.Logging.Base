using System.Collections.Generic;
using AwesomeAssertions;
using FakeItEasy;
using WB.Logging;

namespace LogMessageRegistryTests.MethodTests.IsMatchMethodTests;

public sealed class TheIsMatchMethod
{
    [Test]
    public void ShouldReturnTrueIfNoFiltersAreRegistered()
    {
        // Arrange
        LogMessage logMessage = new()
        {
            Payload = "test",  
        };
        LogMessageFilters logMessageFilters = new();

        // Act
        bool isMatch = logMessageFilters.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeTrue(because: "no filters are registered, so any message should match");
    }

    [Test]
    public void ShouldReturnFalseIfAnyFilterReturnsFalse()
    {
        // Arrange
        LogMessage logMessage = new()
        {
            Payload = "test",
        };
        LogMessageFilters logMessageFilters = new();
        logMessageFilters.Add(_ => false);
        logMessageFilters.Add(_ => true);

        // Act
        bool isMatch = logMessageFilters.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeFalse(because: "the registered filter returns false for the message");
    }

    [Test]
    public void ShouldReturnTrueIfAllFiltersReturnTrue()
    {
        // Arrange
        LogMessage logMessage = new()
        {
            Payload = "test",
        };
        LogMessageFilters logMessageFilters = new();
        logMessageFilters.Add(_ => true);
        logMessageFilters.Add(_ => true);

        // Act
        bool isMatch = logMessageFilters.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeTrue(because: "all registered filters return true for the message");
    }
}