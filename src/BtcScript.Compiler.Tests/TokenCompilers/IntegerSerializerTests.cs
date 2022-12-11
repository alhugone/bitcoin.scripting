using System;
using BtcScript.Compiler.Compiling.TokenCompilers;
using FluentAssertions;
using Xunit;

namespace BtcScript.Compiler.Tests.TokenCompilers;

public class IntegerSerializerTests
{
    private Random _random = new();

    [Theory]
    [InlineData(0)]
    [InlineData(0xFFL)]
    [InlineData(0xFFFFL)]
    [InlineData(0xFFFFFFL)]
    [InlineData(0xFFFFFFFFL)]
    [InlineData(0xFFFFFFFFFFL)]
    [InlineData(0xFFFFFFFFFFFFL)]
    [InlineData(0xFFFFFFFFFFFFFFL)]
    [InlineData(-0xFFL)]
    [InlineData(-0xFFFFL)]
    [InlineData(-0xFFFFFFL)]
    [InlineData(-0xFFFFFFFFL)]
    [InlineData(-0xFFFFFFFFFFL)]
    [InlineData(-0xFFFFFFFFFFFFL)]
    [InlineData(-0xFFFFFFFFFFFFFFL)]
    public void Test(long expected)
    {
        IntegerSerializer.Deserialize(IntegerSerializer.Serialize(expected)).Should().Be(expected);
    }
}