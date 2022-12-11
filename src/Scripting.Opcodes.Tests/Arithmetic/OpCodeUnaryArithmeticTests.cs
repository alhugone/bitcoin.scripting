using System;
using System.Collections.Generic;
using FluentAssertions;
using Scripting.Engine;
using Scripting.Engine.BStacks;
using Scripting.Engine.Number;
using Xunit;

namespace Scripting.Opcodes.Tests;

public class OpCodeUnaryArithmeticTests
{
    private static readonly Faker.Faker _faker = new();

    public static IEnumerable<object[]> Increment
    {
        get
        {
            var opCode = OpcodeType.Op1Add;
            var x = _faker.NextInt;
            yield return new object[] { opCode, x, (long)x + 1 };
            yield return new object[] { opCode, 0, 1 };
            yield return new object[] { opCode, BInt32.MaxValue, (long)BInt32.MaxValue + 1 };
        }
    }

    public static IEnumerable<object[]> Decrement
    {
        get
        {
            var opCode = OpcodeType.Op1Sub;
            var x = _faker.NextInt;
            yield return new object[] { opCode, x, (long)x - 1 };
            yield return new object[] { opCode, 0, -1 };
            yield return new object[] { opCode, BInt32.MinValue, (long)BInt32.MinValue - 1 };
        }
    }

    public static IEnumerable<object[]> IsNotZero
    {
        get
        {
            var opCode = OpcodeType.Op0NotEqual;
            var x = _faker.NextInt;
            yield return new object[] { opCode, x, x != 0 };
            yield return new object[] { opCode, 0, 0 };
            yield return new object[] { opCode, 1, 1 };
        }
    }

    public static IEnumerable<object[]> IsZero
    {
        get
        {
            var opCode = OpcodeType.OpNot;
            var x = _faker.NextInt;
            yield return new object[] { opCode, x, x == 0 };
            yield return new object[] { opCode, 0, 1 };
            yield return new object[] { opCode, 1, 0 };
        }
    }

    public static IEnumerable<object[]> Abs
    {
        get
        {
            var opCode = OpcodeType.OpAbs;
            var x = _faker.NextInt;
            yield return new object[] { opCode, x, Math.Abs(x) };
            yield return new object[] { opCode, 0, 0 };
            yield return new object[] { opCode, -1, 1 };
            yield return new object[] { opCode, 1, 1 };
        }
    }

    public static IEnumerable<object[]> Negate
    {
        get
        {
            var opCode = OpcodeType.OpNegate;
            var x = _faker.NextInt;
            yield return new object[] { opCode, x, -x };
            yield return new object[] { opCode, 0, 0 };
            yield return new object[] { opCode, -1, 1 };
            yield return new object[] { opCode, 1, -1 };
        }
    }

    [Theory]
    [MemberData(nameof(Increment))]
    [MemberData(nameof(Decrement))]
    [MemberData(nameof(IsNotZero))]
    [MemberData(nameof(IsZero))]
    [MemberData(nameof(Abs))]
    [MemberData(nameof(Negate))]
    public void OpCodeShouldReturnExpectedResultAndDoExpectedStackOperations(
        OpcodeType opcode,
        int x,
        long expected
    )
    {
        var script = $"{x} {opcode}";
        var stack = new CountingBStack(new BStack());
        var engine = BuildBScriptEngineFactory.BuildBScriptEngine(script, stack);
        // act
        engine.Execute();
        // assert
        stack.Count.Should().Be(1, "Result should be pushed on Stack");
        stack.PushInvoked.Should().Be(2);
        stack.PopInvoked.Should().Be(1, "Two elements for Binary operation");
        var result = engine.Stack.PeekInt64().ToString();
        long.Parse(result).Should().Be(expected);
    }
}