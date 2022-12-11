using System.Collections.Generic;
using FluentAssertions;
using Scripting.Engine;
using Scripting.Engine.BStacks;
using Xunit;

namespace Scripting.Opcodes.Tests;

public class OpCode3ArgsArithmeticTests
{
    private static readonly Faker.Faker Faker = new();

    public static IEnumerable<object[]> Within
    {
        get
        {
            var opCode = OpcodeType.OpWithin;
            var x = Faker.NextInt;
            var y = Faker.NextInt;
            var z = Faker.NextInt;
            yield return new object[] { opCode, x, y, z, y <= x && x <= z };
            yield return new object[] { opCode, 0, 0, 0, 1 };
            yield return new object[] { opCode, 0, 0, 1, 1 };
            yield return new object[] { opCode, 0, 1, 1, 0 };
            yield return new object[] { opCode, 0, 1, 0, 0 };
            yield return new object[] { opCode, 0, -1, 0, 1 };
            yield return new object[] { opCode, 0, -1, 1, 1 };
        }
    }

    [Theory]
    [MemberData(nameof(Within))]
    public void BinaryOpCodeShouldReturnExpectedResultAndDoExpectedStackOperations(
        OpcodeType opcode,
        int x,
        int y,
        int z,
        long expected
    )
    {
        var script = $"{x} {y} {z} {opcode}";
        var stack = new CountingBStack(new BStack());
        var engine = BuildBScriptEngineFactory.BuildBScriptEngine(script, stack);
        // act
        engine.Execute();
        // assert
        stack.Count.Should().Be(1, "Result should be pushed on Stack");
        stack.PushInvoked.Should().Be(4);
        stack.PopInvoked.Should().Be(3, "Two elements for Binary operation");
        var result = engine.Stack.PeekInt64().ToString();
        long.Parse(result).Should().Be(expected);
    }
}