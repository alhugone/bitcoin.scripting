using System.Collections.Generic;
using FluentAssertions;
using Scripting.Engine;
using Scripting.Engine.BStacks;
using Xunit;

namespace Scripting.Opcodes.Tests;

public class CodeCompareTests
{
    private static readonly Faker.Faker Faker = new();

    public static IEnumerable<object[]> GreaterThan
    {
        get
        {
            var opCode = OpcodeType.OpGreaterThan;
            long x = Faker.NextInt;
            long y = Faker.NextInt;
            yield return new object[] { opCode, x, y, x < y ? 1 : 0 };
            yield return new object[] { opCode, 0, 0, 0 };
        }
    }

    public static IEnumerable<object[]> OpGreaterThanOrEqual
    {
        get
        {
            var opCode = OpcodeType.OpGreaterThanOrEqual;
            long x = Faker.NextInt;
            long y = Faker.NextInt;
            yield return new object[] { opCode, x, y, x <= y ? 1 : 0 };
            yield return new object[] { opCode, 0, 0, 1 };
        }
    }

    public static IEnumerable<object[]> OpLessThan
    {
        get
        {
            var opCode = OpcodeType.OpLessThan;
            long x = Faker.NextInt;
            long y = Faker.NextInt;
            yield return new object[] { opCode, x, y, x > y ? 1 : 0 };
            yield return new object[] { opCode, 0, 0, 0 };
        }
    }

    public static IEnumerable<object[]> OpLessThanOrEqual
    {
        get
        {
            var opCode = OpcodeType.OpLessThanOrEqual;
            long x = Faker.NextInt;
            long y = Faker.NextInt;
            yield return new object[] { opCode, x, y, x >= y ? 1 : 0 };
            yield return new object[] { opCode, 0, 0, 1 };
        }
    }

    public static IEnumerable<object[]> OpNumEqual
    {
        get
        {
            var opCode = OpcodeType.OpNumEqual;
            long x = Faker.NextInt;
            long y = Faker.NextInt;
            yield return new object[] { opCode, x, y, x == y ? 1 : 0 };
            yield return new object[] { opCode, 0, 0, 1 };
        }
    }

    public static IEnumerable<object[]> OpNumNotEqual
    {
        get
        {
            var opCode = OpcodeType.OpNumNotEqual;
            long x = Faker.NextInt;
            long y = Faker.NextInt;
            yield return new object[] { opCode, x, y, x != y ? 1 : 0 };
            yield return new object[] { opCode, 0, 0, 0 };
        }
    }

    public static IEnumerable<object[]> OpBoolOr
    {
        get
        {
            var opCode = OpcodeType.OpBoolOr;
            long x = Faker.NextInt;
            long y = Faker.NextInt;
            yield return new object[] { opCode, x, y, x != y ? 1 : 0 };
            yield return new object[] { opCode, 0, 0, 0 };
            yield return new object[] { opCode, 1, 0, 1 };
            yield return new object[] { opCode, 0, 1, 1 };
            yield return new object[] { opCode, 1, 1, 1 };
        }
    }

    public static IEnumerable<object[]> OpBoolAnd
    {
        get
        {
            var opCode = OpcodeType.OpBoolAnd;
            yield return new object[] { opCode, 0, 0, 0 };
            yield return new object[] { opCode, 1, 0, 0 };
            yield return new object[] { opCode, 0, 1, 0 };
            yield return new object[] { opCode, 1, 1, 1 };
        }
    }

    [Theory]
    [MemberData(nameof(GreaterThan))]
    [MemberData(nameof(OpGreaterThanOrEqual))]
    [MemberData(nameof(OpLessThan))]
    [MemberData(nameof(OpLessThanOrEqual))]
    [MemberData(nameof(OpNumEqual))]
    [MemberData(nameof(OpNumNotEqual))]
    [MemberData(nameof(OpBoolOr))]
    [MemberData(nameof(OpBoolAnd))]
    public void OpCodeShouldReturnExpectedResultAndDoExpectedStackOperations(
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