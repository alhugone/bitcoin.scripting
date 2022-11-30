using System;
using System.Collections.Generic;
using System.Linq;
using BtcScript.Compiler.Compiling.Parsing.Tokens;
using BtcScript.Compiler.Compiling.TokenCompilers;
using FluentAssertions;
using Xunit;

namespace BtcScript.Compiler.Tests;

public class BNumberCompilerTests
{
    private readonly BNumberCompiler _cut = new();

    public static IEnumerable<object[]> NumbersFrom1To16
    {
        get
        {
            for (var i = 0; i < 16; i++)
                yield return new object[] { (i + 1).ToString(), i + OpCodeType.OP_1 };
        }
    }

    [Theory]
    [InlineData(0x1FL)]
    [InlineData(0x01FFL)]
    [InlineData(0x01FFFFL)]
    [InlineData(0x1FFFFFL)]
    [InlineData(0x01FFFFFFL)]
    [InlineData(0x1FFFFFFFL)]
    public void WithPositiveNumberShouldReturnMinimalByteRepresentation(long value)
    {
        var expectedBytes = BitConverter.GetBytes(value).Reverse().ToArray();
        expectedBytes = expectedBytes.SkipWhile(b => b == 0).Reverse().ToArray();
        _cut.ToByteCode(new BNumber(value.ToString()))
            .Should()
            .BeEquivalentTo(expectedBytes);
    }

    [Theory]
    [InlineData(-0x1FL)]
    [InlineData(-0xFFL)]
    [InlineData(-0x1FFFL)]
    [InlineData(-0xFFFFL)]
    [InlineData(-0x01FFFFL)]
    [InlineData(-0x0FFFFFL)]
    [InlineData(-0x1FFFFFL)]
    [InlineData(-0xFFFFFFL)]
    public void WithNegativeNumberShouldReturnMinimalByteRepresentation(long value)
    {
        var expectedBytes = BitConverter.GetBytes(-value).Reverse().ToArray();
        expectedBytes = expectedBytes.SkipWhile(b => b == 0).Reverse().ToArray();
        if ((expectedBytes[^1] & 0x80) == 0)
            expectedBytes[^1] |= 0x80;
        else
            expectedBytes = expectedBytes.Append((byte)0x80).ToArray();
        _cut.ToByteCode(new BNumber(value.ToString()))
            .Should()
            .BeEquivalentTo(expectedBytes);
    }

    [Theory]
    [InlineData("-1", OpCodeType.OP_1NEGATE)]
    [InlineData("0", OpCodeType.OP_0)]
    [MemberData(nameof(NumbersFrom1To16))]
    public void ForNumberBetweenMinusOneAndSixteen_ReturnsOneByteThatRepresentsOpCode(string input, OpCodeType expected)
    {
        _cut.ToByteCode(new BNumber(input))
            .Should()
            .BeEquivalentTo(new[] { (byte)expected });
    }
}
