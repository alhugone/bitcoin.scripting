using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Scripting.OpCodes;
using Scripting.OpCodes.BStacks;
using Scripting.OpCodes.Number;
using Xunit;

namespace Scripting.Opcodes.Tests
{
    public class OpCodeStackTests
    {
        private static readonly Faker.Faker _faker = new();

        public static IEnumerable<object[]> OpDup
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.OpDup,
                    BStackBuilder.WithItems("1","2","3"),
                    BStackBuilder.WithItems("1","2","3","3")
                };
            }
        }

        public static IEnumerable<object[]> Op2Dup
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.Op2Dup,
                    BStackBuilder.WithItems("1","2","3"),
                    BStackBuilder.WithItems("1","2","3","2","3")
                };
            }
        }

        public static IEnumerable<object[]> Op3Dup
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.Op3Dup,
                    BStackBuilder.WithItems("1","2","3"),
                    BStackBuilder.WithItems("1","2","3","1","2","3")
                };
            }
        }

        public static IEnumerable<object[]> OpDrop
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.OpDrop,
                    BStackBuilder.WithItems("1","2","3"),
                    BStackBuilder.WithItems("1","2")
                };
            }
        }

        public static IEnumerable<object[]> Op2Drop
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.Op2Drop,
                    BStackBuilder.WithItems("1","2","3"),
                    BStackBuilder.WithItems("1")
                };
            }
        }

        public static IEnumerable<object[]> OpNip
        {
            get
            {
                var expected=new[]{"1","2"};
                var init = expected.ToList();
                init.AddRange(_fixture.CreateMany<string>());
                yield return new object[]
                {
                    OpcodeType.OpNip,
                    BStackBuilder.WithItems(init.ToArray()),
                    BStackBuilder.WithItems(expected)
                };
            }
        }

        public static IEnumerable<object[]> OpOver
        {
            get
            {

                var init = _fixture.CreateMany<string>().ToArray();
                var expected = init.Append(init[^2]).ToArray();

                yield return new object[]
                {
                    OpcodeType.OpOver,
                    BStackBuilder.WithItems(init.ToArray()),
                    BStackBuilder.WithItems(expected)
                };
            }
        }

        private static Random rand = new Random();
        public static IEnumerable<object[]> OpPick
        {
            get
            {
                yield return new object[]
                {
                    OpcodeType.OpPick,
                    BStackBuilder.WithItems("1","2","3","4","5").PushItem(4),
                    BStackBuilder.WithItems("1","2","3","4","5","2")
                };
                yield return new object[]
                {
                    OpcodeType.OpPick,
                    BStackBuilder.WithItems("1","2","3","4","5").PushItem(5),
                    BStackBuilder.WithItems("1","2","3","4","5","1")
                };

                yield return new object[]
                {
                    OpcodeType.OpPick,
                    BStackBuilder.WithItems("1","2","3","4","5").PushItem(1),
                    BStackBuilder.WithItems("1","2","3","4","5","5")
                };
                // var count = rand.Next(1, 100);
                // var init = _fixture.CreateMany<IReadOnlyList<byte>>(count).ToArray();
                // var copyIndex = rand.Next(1,count);
                // init = init.Append(BitConverter.GetBytes(copyIndex).ToList()).ToArray();
                // var expected = init.SkipLast(1).Append(init[^(copyIndex+1)]).ToArray();
                //
                // yield return new object[]
                // {
                //     OpcodeType.OpPick,
                //     BStackBuilder.WithItems(init),
                //     BStackBuilder.WithItems(expected)
                // };
            }
        }

        public static IEnumerable<object[]> OpRoll
        {
            get
            {
                var opcode = OpcodeType.OpRoll;
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5").PushItem(1),
                    BStackBuilder.WithItems("1","2","3","4","5")
                };

                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5").PushItem(2),
                    BStackBuilder.WithItems("1","2","3","5","4")
                };
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5").PushItem(3),
                    BStackBuilder.WithItems("1","2","4","5","3")
                };

                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5").PushItem(4),
                    BStackBuilder.WithItems("1","3","4","5","2")
                };
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5").PushItem(5),
                    BStackBuilder.WithItems("2","3","4","5","1")
                };
            }
        }

        public static IEnumerable<object[]> OpRot
        {
            get
            {
                var opcode = OpcodeType.OpRot;
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5"),
                    BStackBuilder.WithItems("1","2","5","3","4")
                };

                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3"),
                    BStackBuilder.WithItems("3","1","2")
                };
            }
        }


        public static IEnumerable<object[]> OpSwap
        {
            get
            {
                var opcode = OpcodeType.OpSwap;
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5"),
                    BStackBuilder.WithItems("1","2","3","5","4")
                };

                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2"),
                    BStackBuilder.WithItems("2","1")
                };
            }
        }


        public static IEnumerable<object[]> OpTuck
        {
            get
            {
                var opcode = OpcodeType.OpTuck;
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5"),
                    BStackBuilder.WithItems("1","2","3","5","4","5")
                };

                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2"),
                    BStackBuilder.WithItems("2","1","2")
                };
            }
        }

        public static IEnumerable<object[]> Op2Over
        {
            get
            {
                var opcode = OpcodeType.Op2Over;
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5"),
                    BStackBuilder.WithItems("1","2","3","4","5","2","3")
                };
            }
        }


        public static IEnumerable<object[]> Op2Rot
        {
            get
            {
                var opcode = OpcodeType.Op2Rot;
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4","5","6"),
                    BStackBuilder.WithItems("3","4","5","6","1","2")
                };
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("0","0","1","2","3","4","5","6"),
                    BStackBuilder.WithItems("0","0","3","4","5","6","1","2")
                };
            }
        }

        public static IEnumerable<object[]> Op2Swap
        {
            get
            {
                var opcode = OpcodeType.Op2Swap;
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2","3","4"),
                    BStackBuilder.WithItems("3","4","1","2")
                };
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("0","0","1","2","3","4"),
                    BStackBuilder.WithItems("0","0","3","4","1","2")
                };
            }
        }


        public static IEnumerable<object[]> OpIfdup
        {
            get
            {
                var opcode = OpcodeType.OpIfdup;
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1").PushItem(0),
                    BStackBuilder.WithItems("1").PushItem(0)
                };
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1"),
                    BStackBuilder.WithItems("1","1")
                };
                yield return new object[]
                {
                    opcode,
                    BStackBuilder.WithItems("1","2"),
                    BStackBuilder.WithItems("1","2","2")
                };
            }
        }


        public static IEnumerable<object[]> OpDepth
        {
            get
            {
                var opcode = OpcodeType.OpDepth;
                var array = _fixture.CreateMany<string>().ToArray();
                var init = BStackBuilder.WithItems(array);
                var expected = BStackBuilder.WithItems(array);
                expected.Push(new BInt32(init.Count));
                yield return new object[]
                {
                    opcode,
                    init,
                    expected
                };
            }
        }



        private static readonly Fixture _fixture = new Fixture();
        public static IReadOnlyList<byte> GetRandom()
        {
            return _fixture.CreateMany<byte>().ToList();
        }

        [Theory]
        [MemberData(nameof(OpDup))]
        [MemberData(nameof(Op2Dup))]
        [MemberData(nameof(Op3Dup))]
        [MemberData(nameof(OpDrop))]
        [MemberData(nameof(Op2Drop))]
        [MemberData(nameof(OpNip))]
        [MemberData(nameof(OpOver))]
        [MemberData(nameof(OpPick))]
        [MemberData(nameof(OpRoll))]
        [MemberData(nameof(OpRot))]
        [MemberData(nameof(OpSwap))]
        [MemberData(nameof(OpTuck))]
        [MemberData(nameof(Op2Over))]
        [MemberData(nameof(Op2Rot))]
        [MemberData(nameof(Op2Swap))]
        [MemberData(nameof(OpIfdup))]
        [MemberData(nameof(OpDepth))]
        public void BinaryOpCodeShouldReturnExpectedResultAndDoExpectedStackOperations(
            OpcodeType opcode,
            BStack initial,
            BStack expected
        )
        {
            var script = $"{opcode}";
            var stack = initial;
            var engine = new BScriptEngine(script, OpLocatorFactory.CreateStandard(), stack);
            // act
            engine.Execute();
            // assert
            initial.Should().BeEquivalentTo(expected);
            var i = initial.Count;
            while (initial.Count != 0)
            {
                initial.ShouldBeEquivalentTo(expected);
                i--;
            }
        }
    }
}