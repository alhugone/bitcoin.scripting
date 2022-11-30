using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using BtcScript.Compiler.Compiling.TokenCompilers;
using FluentAssertions;
using Xunit;

namespace BtcScript.Compiler.Tests;

public class ByteCompilerTests
{
    private static readonly Fixture Fixture = new();
    private static readonly Random Random = new();
    private readonly BytesCompiler _cut = new();

    public static IEnumerable<object[]> ArrayOfBytesFrom1To75LengthShouldBePrefixedWithLengthOnByte
    {
        get
        {
            for (var i = 1; i <= 75; i++)
                yield return new object[] { Fixture.CreateMany<byte>(i).ToArray(), new[] { (byte)i } };
        }
    }

    public static IEnumerable<object[]>
        ArrayOfBytesFrom76To0xFFLengthShouldBePrefixedWithTwoBytesOP_PUSHDATA1AndOneByteOfLength
    {
        get
        {
            const int from = 76;
            const int to = 0xFF;
            var listOfLengths = Enumerable.Range(0, 10)
                .Select(_ => Random.Next(from + 1, to - 1))
                .Append(from)
                .Append(to);
            foreach (var length in listOfLengths)
                yield return new object[]
                {
                    Fixture.CreateMany<byte>(length).ToArray(), new[] { (byte)OpCodeType.OP_PUSHDATA1, (byte)length }
                };
        }
    }

    public static IEnumerable<object[]>
        ArrayOfBytesFrom0x0100To0xFFFFLengthShouldBePrefixedWithThreeBytesOP_PUSHDATA2AndTwoBytesOfLength
    {
        get
        {
            const int from = 0x0100;
            const int to = 0xffff;
            var listOfLengths = Enumerable.Range(0, 10)
                .Select(_ => Random.Next(from + 1, to - 1))
                .Append(from)
                .Append(to);
            foreach (var length in listOfLengths)
            {
                var lengthBytes = BitConverter.GetBytes((short)length);
                var prefix = new[] { (byte)OpCodeType.OP_PUSHDATA2 }.Concat(lengthBytes).ToArray();
                yield return new object[]
                {
                    Fixture.CreateMany<byte>(length).ToArray(),
                    prefix
                };
            }
        }
    }

    public static IEnumerable<object[]>
        ArrayOfBytesFrom0x010000To0xFFFFFFLengthShouldBePrefixedWithFiveBytesOP_PUSHDATA4AndFourBytesOfLength
    {
        get
        {
            const int from = 0x010000;
            const int to = 0xffffff;
            var listOfLengths = Enumerable.Range(0, 0)
                .Append(from)
                .Append(to);
            foreach (var length in listOfLengths)
            {
                var lengthBytes = BitConverter.GetBytes(length);
                var prefix = new[] { (byte)OpCodeType.OP_PUSHDATA4 }.Concat(lengthBytes).ToArray();
                yield return new object[]
                {
                    Fixture.CreateMany<byte>(length).ToArray(),
                    prefix
                };
            }
        }
    }

    [Theory]
    [MemberData(nameof(ArrayOfBytesFrom1To75LengthShouldBePrefixedWithLengthOnByte))]
    [MemberData(nameof(ArrayOfBytesFrom76To0xFFLengthShouldBePrefixedWithTwoBytesOP_PUSHDATA1AndOneByteOfLength))]
    [MemberData(
        nameof(ArrayOfBytesFrom0x0100To0xFFFFLengthShouldBePrefixedWithThreeBytesOP_PUSHDATA2AndTwoBytesOfLength)
    )]
    [MemberData(
        nameof(ArrayOfBytesFrom0x010000To0xFFFFFFLengthShouldBePrefixedWithFiveBytesOP_PUSHDATA4AndFourBytesOfLength)
    )]
    public void ShouldPrefixDataWithExpectOpCodeAndOrInteger(byte[] input, byte[] expectedPrefixAdded) =>
        _cut.ToByteCode(input).Should().BeEquivalentTo(expectedPrefixAdded.Concat(input));
}
