// ┌────────────────────────────────────────────────────────────────────────────────┐
// │ File: PipelineBehaviorTests.cs                                                 │
// │ Author: GitHub Copilot                                                         │
// │ Created: 2026-11-12                                                            │
// └────────────────────────────────────────────────────────────────────────────────┘
using System;
using AwesomeAssertions;
using FakeItEasy;
using WB.Logging;

namespace LogMessageFiltersTests.MethodTests;

/// <summary>Checks filter pipeline behavior across registration changes.</summary>
public sealed class PipelineBehaviorTests
{
    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>Checks that filters receive the submitted message and evaluate its payload.</summary>
    [Test]
    public void IsMatch_PayloadPredicate_UsesEachSubmittedMessage()
    {
        ILogMessage allowed = A.Fake<ILogMessage>();
        ILogMessage blocked = A.Fake<ILogMessage>();
        A.CallTo(() => allowed.Payload).Returns("allow");
        A.CallTo(() => blocked.Payload).Returns("deny");
        LogMessageFilterPipeline pipeline = new();
        pipeline.Add(message => Equals(message.Payload, "allow"));

        pipeline.IsMatch(allowed).Should().BeTrue();
        pipeline.IsMatch(blocked).Should().BeFalse();
    }

    /// <summary>Checks recompilation after adding a filter to a previously used pipeline.</summary>
    [Test]
    public void Add_AfterMatching_UpdatesCombinedResult()
    {
        ILogMessage message = A.Fake<ILogMessage>();
        LogMessageFilterPipeline pipeline = new();
        pipeline.Add(_ => true);
        pipeline.IsMatch(message).Should().BeTrue();

        pipeline.Add(_ => false);

        pipeline.IsMatch(message).Should().BeFalse();
    }

    /// <summary>Checks that disposing registrations removes only their own filters.</summary>
    [Test]
    public void Dispose_Registrations_RemoveTheirFiltersAndRestoreMatchAll()
    {
        ILogMessage message = A.Fake<ILogMessage>();
        LogMessageFilterPipeline pipeline = new();
        IDisposable rejectFirst = pipeline.Add(_ => false);
        IDisposable rejectSecond = pipeline.Add(_ => false);
        pipeline.IsMatch(message).Should().BeFalse();

        rejectFirst.Dispose();
        pipeline.IsMatch(message).Should().BeFalse();

        rejectSecond.Dispose();
        pipeline.IsMatch(message).Should().BeTrue();
    }

    /// <summary>Checks conjunction stops on the first rejection, independent of filter order.</summary>
    [Test]
    public void IsMatch_RejectingFilters_ShortCircuits()
    {
        ILogMessage message = A.Fake<ILogMessage>();
        LogMessageFilterPipeline pipeline = new();
        int calls = 0;
        pipeline.Add(_ => { calls++; return false; });
        pipeline.Add(_ => { calls++; return false; });

        pipeline.IsMatch(message).Should().BeFalse();
        calls.Should().Be(1);
    }

    /// <summary>Checks that predicate failures are not swallowed.</summary>
    [Test]
    public void IsMatch_FilterThrows_PropagatesException()
    {
        ILogMessage message = A.Fake<ILogMessage>();
        LogMessageFilterPipeline pipeline = new();
        InvalidOperationException failure = new("filter failure");
        pipeline.Add(_ => throw failure);
        Action act = () => pipeline.IsMatch(message);

        act.Should().Throw<InvalidOperationException>().Which.Should().BeSameAs(failure);
    }
}
