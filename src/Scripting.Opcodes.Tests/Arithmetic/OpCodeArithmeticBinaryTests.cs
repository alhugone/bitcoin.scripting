using System;
using System.Collections.Generic;
using FluentAssertions;
using Scripting.Engine;
using Scripting.Engine.BStacks;
using Scripting.Engine.Number;
using Xunit;

namespace Scripting.Opcodes.Tests;

public class OpCodeArithmeticBinaryTests
{
    private static readonly Faker.Faker _faker = new();

    public static IEnumerable<object[]> Add
    {
        get
        {
            var opCode = OpcodeType.OpAdd;
            var x = _faker.NextInt;
            var y = _faker.NextInt;
            yield return new object[] { opCode, x, y, (long)x + y };
            yield return new object[] { opCode, 0, 0, 0 };
            yield return new object[] { opCode, int.MaxValue, int.MaxValue, 4294967294L };
        }
    }

    public static IEnumerable<object[]> Sub
    {
        get
        {
            var opCode = OpcodeType.OpSub;
            long x = _faker.NextInt;
            long y = _faker.NextInt;
            yield return new object[] { opCode, x, y, x - y };
            yield return new object[] { opCode, 0, 0, 0 };
            yield return new object[] { opCode, BInt32.MaxValue, BInt32.MaxValue, 0 };
            yield return new object[]
                { opCode, BInt32.MinValue, BInt32.MinValue, (long)BInt32.MinValue - BInt32.MinValue };
        }
    }

    public static IEnumerable<object[]> Max
    {
        get
        {
            var opCode = OpcodeType.OpMax;
            long x = _faker.NextInt;
            long y = _faker.NextInt;
            yield return new object[] { opCode, x, y, Math.Max(x, y) };
            yield return new object[] { opCode, 0, 0, 0 };
        }
    }

    public static IEnumerable<object[]> Min
    {
        get
        {
            var opCode = OpcodeType.OpMin;
            long x = _faker.NextInt;
            long y = _faker.NextInt;
            yield return new object[] { opCode, x, y, Math.Min(x, y) };
            yield return new object[] { opCode, 0, 0, 0 };
        }
    }

    [Theory]
    [MemberData(nameof(Add))]
    [MemberData(nameof(Sub))]
    [MemberData(nameof(Max))]
    [MemberData(nameof(Min))]
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
}