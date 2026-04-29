using System;
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
        ILogMessage<object> logMessage = A.Fake<ILogMessage<object>>();
        LogMessageFilterRegistry registry = new();

        // Act
        bool isMatch = registry.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeTrue(because: "no filters are registered, so any message should match");
    }

    [Test]
    public void ShouldReturnFalseIfAnyFilterReturnsFalse()
    {
        // Arrange
        ILogMessage<object> logMessage = A.Fake<ILogMessage<object>>();
        LogMessageFilterRegistry registry = new();
        registry.RegisterLogMessageFilter<object>(_ => false);
        registry.RegisterLogMessageFilter<object>(_ => true);

        // Act
        bool isMatch = registry.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeFalse(because: "the registered filter returns false for the message");
    }

    [Test]
    public void ShouldReturnTrueIfAllFiltersReturnTrue()
    {
        // Arrange
        ILogMessage<object> logMessage = A.Fake<ILogMessage<object>>();
        LogMessageFilterRegistry registry = new();
        registry.RegisterLogMessageFilter<object>(_ => true);
        registry.RegisterLogMessageFilter<object>(_ => true);

        // Act
        bool isMatch = registry.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeTrue(because: "all registered filters return true for the message");
    }

    [Test]
    public void ShouldSupportInheritance()
    {
        // Arrange
        ILogMessage<string> logMessage = A.Fake<ILogMessage<string>>();
        LogMessageFilterRegistry registry = new();
        registry.RegisterLogMessageFilter<object>(_ => false);

        // Act
        bool isMatch = registry.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeFalse(because: "the registered filter for the base type returns false for the message");
    }

    [Test]
    public void ShouldSupportInterfaces()
    {
        // Arrange
        ILogMessage<string> logMessage = A.Fake<ILogMessage<string>>();
        LogMessageFilterRegistry registry = new();
        registry.RegisterLogMessageFilter<IEnumerable<char>>(_ => false);

        // Act
        bool isMatch = registry.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeFalse(because: "the registered filter for the implemented interface returns false for the message");
    }
}