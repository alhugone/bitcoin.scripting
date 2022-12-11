using System;
using System.Collections.Generic;
using FluentAssertions;
using Scripting.Engine;
using Scripting.Engine.BStacks;
using Xunit;

namespace Scripting.Opcodes.Tests;

public class OpCodeBitTests
{
    public static IEnumerable<object[]> OpEqual
    {
        get
        {
            var opCode = OpcodeType.OpEqual;
            yield return new object[] { opCode, 0, 0, 1 };
            yield return new object[] { opCode, 1, 0, 0 };
            yield return new object[] { opCode, 0, 1, 0 };
            yield return new object[] { opCode, 1, 1, 1 };
        }
    }

    public static IEnumerable<object[]> OpEqualVerifyValid
    {
        get
        {
            var rand = new Random();
            var same = rand.Next();
            var opCode = OpcodeType.OpEqualVerify;
            yield return new object[] { opCode, 0, 0 };
            yield return new object[] { opCode, 1, 1 };
            yield return new object[] { opCode, same, same };
        }
    }

    public static IEnumerable<object[]> OpEqualVerifyInvalid
    {
        get
        {
            var opCode = OpcodeType.OpEqualVerify;
            yield return new object[] { opCode, 0, 1 };
            yield return new object[] { opCode, 1, 0 };
        }
    }

    [Theory]
    [MemberData(nameof(OpEqual))]
    public void BinaryOpCodeShouldReturnExpectedResultAndDoExpectedStackOperations(
        OpcodeType opcode,
        int x,
        int y,
        long expected
    )
    {
        var script = $"{x} {y} {opcode}";
        var stack = new CountingBStack(new BStack());
        var engine = BuildBScriptEngineFactory.BuildBScriptEngine(script, stack);
        // act
        engine.Execute();
        // assert
        stack.Count.Should().Be(1, "Result should be pushed on Stack");
        stack.PushInvoked.Should().Be(3);
        stack.PopInvoked.Should().Be(2, "Two elements for Binary operation");
        var result = engine.Stack.PeekInt64().ToString();
        long.Parse(result).Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(OpEqualVerifyValid))]
    public void OpEqualVerifyWithTwoEqualValuesShouldReturnEmptyStack(
        OpcodeType opcode,
        int x,
        int y
    )
    {
        var script = $"{x} {y} {opcode}";
        var stack = new CountingBStack(new BStack());
        var engine = BuildBScriptEngineFactory.BuildBScriptEngine(script, stack);
        // act
        engine.Execute();
        // assert
        stack.Count.Should().Be(0, "Result should be pushed on Stack");
        stack.PushInvoked.Should().Be(3);
        stack.PopInvoked.Should().Be(3, "Verify if succeed pops result");
    }

    [Theory]
    [MemberData(nameof(OpEqualVerifyInvalid))]
    public void OpEqualVerifyShouldThrow(
        OpcodeType opcode,
        int x,
        int y
    )
    {
        var script = $"{x} {y} {opcode}";
        var stack = new CountingBStack(new BStack());
        var engine = BuildBScriptEngineFactory.BuildBScriptEngine(script, stack);
        // act
        Assert.ThrowsAny<Exception>(() => engine.Execute());
    }
}