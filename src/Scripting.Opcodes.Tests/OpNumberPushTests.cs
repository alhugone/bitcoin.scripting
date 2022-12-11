using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Scripting.Engine;
using Scripting.Engine.BStacks;
using Scripting.Engine.Number;
using Xunit;

namespace Scripting.Opcodes.Tests;

public class OpNumberPushTests
{
    private static readonly Faker.Faker _faker = new();
    private static readonly Fixture _fixture = new();

    public static IEnumerable<object[]> OneByte
    {
        get
        {
            var expected = _fixture.Create<byte>();
            yield return new object[]
            {
                expected.ToString(),
                BStackBuilder.WithItems(new BInt32(expected).Get().ToArray())
            };
            expected = 0;
            yield return new object[]
            {
                expected.ToString(),
                BStackBuilder.WithItems(new BInt32(expected).Get().ToArray())
            };
        }
    }

    public static IEnumerable<object[]> Op0
    {
        get
        {
            yield return new object[]
            {
                "0",
                BStackBuilder.WithItems(Array.Empty<byte>())
            };
        }
    }

    public static IEnumerable<object[]> Op1
    {
        get
        {
            yield return new object[]
            {
                "1",
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
                "1",
                new BStack().PushItem(1)
            };
        }
    }

    public static IEnumerable<object[]> Op1Negate
    {
        get
        {
            yield return new object[]
            {
                "-1",
                new BStack().PushItem(-1)
            };
        }
    }

    public static IEnumerable<object[]> OpFrom2To16
    {
        get
        {
            for (var op = 2; op <= 16; op++)
                yield return new object[]
                {
                    op.ToString(),
                    new BStack().PushItem(op)
                };
        }
    }

    public static IEnumerable<object[]> OpPush1
    {
        get
        {
            var random = new Random();
            random.Next('a', 'z');
            var bytes = string.Join(
                "",
                Enumerable.Range(0, 100)
                    .Select(x => "a")
                    .ToList()
            );
            for (var op = OpcodeType.OpPush1; op <= OpcodeType.OpPush75; op++)
            {
                var testbytes = bytes.Substring(0, (int)op);
                yield return new object[]
                {
                    $"'{testbytes}'",
                    BStackBuilder.WithItems($"{testbytes}")
                };
            }
        }
    }

    [Theory]
    [MemberData(nameof(Op0))]
    [MemberData(nameof(Op1))]
    [MemberData(nameof(OpTrue))]
    [MemberData(nameof(Op1Negate))]
    [MemberData(nameof(OpFrom2To16))]
    [MemberData(nameof(OpPush1))]
    public void BinaryOpCodeShouldReturnExpectedResultAndDoExpectedStackOperations(
        string instruction,
        BStack expected
    )
    {
        var script = $"{instruction}";
        var stack = new BStack();
        var engine = BuildBScriptEngineFactory.BuildBScriptEngine(script, stack);
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
