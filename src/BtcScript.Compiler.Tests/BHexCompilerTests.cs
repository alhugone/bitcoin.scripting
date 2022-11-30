using System;
using System.Linq;
using AutoFixture;
using BtcScript.Compiler.Compiling.Parsing;
using BtcScript.Compiler.Compiling.Parsing.Tokens;
using BtcScript.Compiler.Compiling.TokenCompilers;
using FluentAssertions;
using Xunit;

namespace BtcScript.Compiler.Tests;

public class BHexCompilerTests
{
    private readonly Fixture _fixture = new();
    private readonly BHexCompiler _cut = new();

    [Theory]
    [InlineData("")]
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
}
