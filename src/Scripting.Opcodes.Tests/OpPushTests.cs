using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Unicode;
using AutoFixture;
using FluentAssertions;
using Scripting.OpCodes;
using Scripting.OpCodes.BStacks;
using Scripting.OpCodes.Number;
using Xunit;

namespace Scripting.Opcodes.Tests
{

    public class OpPushTests
    {
        private static readonly Faker.Faker _faker = new();

        public static IEnumerable<object[]> OpFalse
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.OpFalse,
                    BStackBuilder.WithItems(Array.Empty<byte>())
                };

            }
        }

        public static IEnumerable<object[]> Op0
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.Op0,
                    BStackBuilder.WithItems(Array.Empty<byte>())
                };

            }
        }public static IEnumerable<object[]> Op1
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.Op1,
                    new BStack().PushItem(1)
                };
            }
        }

        public static IEnumerable<object[]> OpTrue
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.OpTrue,
                    new BStack().PushItem(1)
                };
            }
        }public static IEnumerable<object[]> Op1Negate
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.Op1Negate,
                    new BStack().PushItem(-1)
                };
            }
        }

        public static IEnumerable<object[]> OpFrom2To16
        {
            get
            {
                for (OpcodeType op = OpcodeType.Op2; op <= OpcodeType.Op16; op++)
                {
                    yield return new object[]
                    {
                        op,
                        new BStack().PushItem((int)op-(int)OpcodeType.Op1+1)
                    };
                }
            }
        }

        private static Fixture _fixture = new Fixture();
        public static IEnumerable<object[]> OpImmediately1
        {
            get
            {
                var random = new Random();
                random.Next((int)'a', (int)'z');
                var bytes =string.Join("", Enumerable.Range(0, 100).Select(x => "a")
                    .ToList());
                for (OpcodeType op = OpcodeType.OpImmediately1; op <= OpcodeType.OpImmediately75; op++)
                {
                    var k = (int)op;
                          yield return new object[]
                    {
                        $"{op} {bytes}",
                        BStackBuilder.WithItems($"'{bytes.Substring(0,k)}'")
                    };
                }
            }
        }

        [Theory]
        [MemberData(nameof(Op0))]
        [MemberData(nameof(OpFalse))]
        [MemberData(nameof(Op1))]
        [MemberData(nameof(OpTrue))]
        [MemberData(nameof(Op1Negate))]
        [MemberData(nameof(OpFrom2To16))]
        [MemberData(nameof(OpImmediately1))]
        public void BinaryOpCodeShouldReturnExpectedResultAndDoExpectedStackOperations(
            string instruction,
            BStack expected
        )
        {
            var script = $"{instruction}";
            var stack = new BStack();
            var engine = new BScriptEngine(script, OpLocatorFactory.CreateStandard(), stack);
            // act
            engine.Execute();
            // assert
            var i = stack.Count;
            while (stack.Count != 0)
            {
                stack.ShouldBeEquivalentTo(expected);
                i--;
            }
        }
    }


}
