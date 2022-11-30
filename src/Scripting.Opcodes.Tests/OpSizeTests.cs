using System;
using System.Collections.Generic;
using FluentAssertions;
using Scripting.OpCodes;
using Scripting.OpCodes.BStacks;
using Xunit;

namespace Scripting.Opcodes.Tests
{
    public class OpSizeTests
    {
        [Fact]
        public void OpSizeShouldPutOnStackStackCount()
        {
            var script = $"{OpcodeType.OpSize}";
            var stack = new CountingBStack(new BStack());
            var list = new List<byte>();
            var random = new Random();
            for (var i = 0; i < random.Next(1, 100); i++)
                list.Add(byte.MaxValue);
            stack.Push(list);
            var engine = new BScriptEngine(script, OpLocatorFactory.CreateStandard(), stack);
            // act
            engine.Execute();
            // assert
            stack.Count.Should().Be(2, "Exiting item and result should be pushed on Stack");
            stack.PushInvoked.Should().Be(2);
            stack.PopInvoked.Should().Be(0, "Two elements for Binary operation");
            var result = engine.Stack.PeekInt64().ToString();
            long.Parse(result).Should().Be(list.Count);
        }
    }
}
