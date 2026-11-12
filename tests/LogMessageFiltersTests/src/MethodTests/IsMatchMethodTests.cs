using System;
using System.Collections.Generic;
using AwesomeAssertions;
using WB.Logging;

namespace LogMessageFilterPipelineTests.MethodTests.IsMatchMethodTests;

internal sealed class LogMessage : ILogMessage
{
    public DateTimeOffset Timestamp { get; set; }

    public IReadOnlyList<string> Senders { get; set; } = null!;

    public LogLevel? LogLevel { get; set; }


    public object Payload { get; set; } = null!;
}

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
        LogMessageFilterPipeline logMessagePipeline = new();

        // Act
        bool isMatch = logMessagePipeline.IsMatch(logMessage);

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
        LogMessageFilterPipeline logMessagePipeline = new();
        logMessagePipeline.Add(_ => false);
        logMessagePipeline.Add(_ => true);

        // Act
        bool isMatch = logMessagePipeline.IsMatch(logMessage);

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
        LogMessageFilterPipeline logMessagePipeline = new();
        logMessagePipeline.Add(_ => true);
        logMessagePipeline.Add(_ => true);

        // Act
        bool isMatch = logMessagePipeline.IsMatch(logMessage);

        // Assert
        isMatch.Should().BeTrue(because: "all registered filters return true for the message");
    }
}