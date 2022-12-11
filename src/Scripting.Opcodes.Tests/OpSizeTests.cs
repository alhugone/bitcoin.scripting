using System;
using FluentAssertions;
using Scripting.Engine;
using Scripting.Engine.BStacks;
using Xunit;

namespace Scripting.Opcodes.Tests;

public class OpSizeTests
{
    [Fact]
    public void OpSizeShouldPutOnStackNumberOfItemsOnTheStack()
    {
        var script = $"{OpcodeType.OpSize}";
        var stack = new CountingBStack(new BStack());
        var random = new Random();
        var stackSize = random.Next(1, 100);
        for (var i = 0; i < stackSize; i++)
            stack.Push(new[] { byte.MaxValue });
        var engine = BuildBScriptEngineFactory.BuildBScriptEngine(script, stack);
        // act
        engine.Execute();
        // assert
        stack.Count.Should().Be(stackSize + 1, "Exiting item and result should be pushed on Stack");
        stack.PopInvoked.Should().Be(0, "Two elements for Binary operation");
        var result = engine.Stack.PeekInt64().ToString();
        long.Parse(result).Should().Be(stackSize);
    }
}