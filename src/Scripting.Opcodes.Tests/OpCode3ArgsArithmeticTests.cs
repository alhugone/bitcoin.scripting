using System.Collections.Generic;
using FluentAssertions;
using Scripting.OpCodes;
using Scripting.OpCodes.BStacks;
using Xunit;

namespace Scripting.Opcodes.Tests
{
    public class OpCode3ArgsArithmeticTests
    {
        private static readonly Faker.Faker _faker = new();

        public static IEnumerable<object[]> Within
        {
            get
            {
                var opCode = OpcodeType.OpWithin;
                var x = _faker.NextInt;
                var y = _faker.NextInt;
                var z = _faker.NextInt;
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
            int
                y,
            int z,
            long expected
        )
        {
            var script = $"{x} {y} {z} {opcode}";
            var stack = new CountingBStack(new BStack());
            var engine = new BScriptEngine(script, OpLocatorFactory.CreateStandard(), stack);
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
}