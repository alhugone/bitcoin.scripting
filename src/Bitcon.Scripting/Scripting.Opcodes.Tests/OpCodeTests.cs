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

        public static IEnumerable<object[]> RandomData
        {
            get
            {
                var x = _faker.NextInt;
                var y = _faker.NextInt;
                yield return new object[] { x, y, (long)x + y };
            }
        }

        [Theory]
        [MemberData(nameof(RandomData))]
        [InlineData(0, 0, 0)]
        [InlineData(int.MaxValue, int.MaxValue, 4294967294L)]
        public void OpAdd_Should_Add_Two_Ints_And_Leave_On_Stack_Result(int x, int y, long expected)
        {
            var script = $"{x} {y} {OpcodeType.OpAdd}";
            var stack = new CountingBStack(new BStack());
            var engine = new BScriptEngine(script, OpLocatorFactory.CreateStandard(), stack);
            // act
            engine.Execute();
            // assert
            stack.Count.Should().Be(1);
            stack.PushInvoked.Should().Be(3);
            stack.PopInvoked.Should().Be(2, "Two elements for Bi operation");
            var result = engine.Stack.PeekInt64().ToString();
            long.Parse(result).Should().Be(expected, $"{x}+{y}={x + (long)y}");
        }
    }
}