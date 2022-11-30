using System;
using System.Collections.Generic;
using FluentAssertions;
using Scripting.OpCodes;
using Scripting.OpCodes.BStacks;
using Xunit;

namespace Scripting.Opcodes.Tests
{
    public class OpCodeTests
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
                yield return new object[] { opCode, int.MaxValue, int.MaxValue, 0 };
                yield return new object[] { opCode, int.MinValue, int.MinValue, (long)int.MinValue - int.MinValue };
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

        public static IEnumerable<object[]> GreaterThan
        {
            get
            {
                var opCode = OpcodeType.OpGreaterThan;
                long x = _faker.NextInt;
                long y = _faker.NextInt;
                yield return new object[] { opCode, x, y, x < y ? 1 : 0 };
                yield return new object[] { opCode, 0, 0, 0 };
            }
        }

        public static IEnumerable<object[]> OpGreaterThanOrEqual
        {
            get
            {
                var opCode = OpcodeType.OpGreaterThanOrEqual;
                long x = _faker.NextInt;
                long y = _faker.NextInt;
                yield return new object[] { opCode, x, y, x <= y ? 1 : 0 };
                yield return new object[] { opCode, 0, 0, 1 };
            }
        }

        public static IEnumerable<object[]> OpLessThan
        {
            get
            {
                var opCode = OpcodeType.OpLessThan;
                long x = _faker.NextInt;
                long y = _faker.NextInt;
                yield return new object[] { opCode, x, y, x > y ? 1 : 0 };
                yield return new object[] { opCode, 0, 0, 0 };
            }
        }

        public static IEnumerable<object[]> OpLessThanOrEqual
        {
            get
            {
                var opCode = OpcodeType.OpLessThanOrEqual;
                long x = _faker.NextInt;
                long y = _faker.NextInt;
                yield return new object[] { opCode, x, y, x >= y ? 1 : 0 };
                yield return new object[] { opCode, 0, 0, 1 };
            }
        }

        public static IEnumerable<object[]> OpNumEqual
        {
            get
            {
                var opCode = OpcodeType.OpNumEqual;
                long x = _faker.NextInt;
                long y = _faker.NextInt;
                yield return new object[] { opCode, x, y, x == y ? 1 : 0 };
                yield return new object[] { opCode, 0, 0, 1 };
            }
        }

        public static IEnumerable<object[]> OpNumNotEqual
        {
            get
            {
                var opCode = OpcodeType.OpNumNotEqual;
                long x = _faker.NextInt;
                long y = _faker.NextInt;
                yield return new object[] { opCode, x, y, x != y ? 1 : 0 };
                yield return new object[] { opCode, 0, 0, 0 };
            }
        }

        public static IEnumerable<object[]> OpBoolOr
        {
            get
            {
                var opCode = OpcodeType.OpBoolOr;
                long x = _faker.NextInt;
                long y = _faker.NextInt;
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
        [MemberData(nameof(Add))]
        [MemberData(nameof(Sub))]
        [MemberData(nameof(Max))]
        [MemberData(nameof(Min))]
        [MemberData(nameof(GreaterThan))]
        [MemberData(nameof(OpGreaterThanOrEqual))]
        [MemberData(nameof(OpLessThan))]
        [MemberData(nameof(OpLessThanOrEqual))]
        [MemberData(nameof(OpNumEqual))]
        [MemberData(nameof(OpNumNotEqual))]
        [MemberData(nameof(OpBoolOr))]
        [MemberData(nameof(OpBoolAnd))]
        public void BinaryOpCodeShouldReturnExpectedResultAndDoExpectedStackOperations(
            OpcodeType opcode,
            int x,
            int y,
            long expected
        )
        {
            var script = $"{x} {y} {opcode}";
            var stack = new CountingBStack(new BStack());
            var engine = new BScriptEngine(script, OpLocatorFactory.CreateStandard(), stack);
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
}