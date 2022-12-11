using System;
using System.Linq;
using AutoFixture;
using BtcScript.Compiler.Compiling.Parsing;
using BtcScript.Compiler.Compiling.Parsing.Tokens;
using BtcScript.Compiler.Compiling.TokenCompilers;
using FluentAssertions;
using Xunit;

namespace BtcScript.Compiler.Tests.TokenCompilers;

public class BHexCompilerTests
{
    private readonly BHexCompiler _cut = new();
    private readonly Fixture _fixture = new();

    [Theory]
    [InlineData("0x")]
    [InlineData("0X")]
    public void Compile_ShouldReturnByteArrayRepresentationOfHexString(string hexPrefix)
    {
        var bytes = _fixture.CreateMany<byte>().ToArray();
        _cut.ToByteCode(new BHex(hexPrefix + Convert.ToHexString(bytes)))
            .Should()
            .BeEquivalentTo(bytes);
    }

    [Fact]
    public void Compile_WithNonHexStringShouldThrow() =>
        Assert.Throws<ParsingException>(() => _cut.ToByteCode(new BHex(_fixture.Create<string>())));

    [Fact]
    public void Compile_WithValidHexStringButNotPrefixedWith0xShouldThrow() =>
        Assert.Throws<ParsingException>(() => _cut.ToByteCode(new BHex("FFAA")));

    [Theory]
    [InlineData("0x1")]
    [InlineData("0xA1F")]
    public void Compile_HexStringWithEvenCharsIsInvalid(string input) =>
        Assert.Throws<ParsingException>(() => _cut.ToByteCode(new BHex(input)));

    [Theory]
    [InlineData("0x")]
    [InlineData("0X")]
    public void Compile_HexPrefixOnlyStringInvalid(string input) =>
        Assert.Throws<ParsingException>(() => _cut.ToByteCode(new BHex(input)));
}