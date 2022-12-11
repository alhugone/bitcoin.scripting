using System.Text;
using AutoFixture;
using BtcScript.Compiler.Compiling.Parsing;
using BtcScript.Compiler.Compiling.Parsing.Tokens;
using BtcScript.Compiler.Compiling.TokenCompilers;
using FluentAssertions;
using Xunit;

namespace BtcScript.Compiler.Tests.TokenCompilers;

public class BStringCompilerTests
{
    private readonly DummyByteCompiler _byteCompiler = new();
    private readonly BStringCompiler _cut;
    private readonly Fixture _fixture = new();
    public BStringCompilerTests() => _cut = new BStringCompiler(_byteCompiler);

    [Theory]
    [InlineData(2 ^ 0)]
    [InlineData(2 ^ 5)]
    [InlineData(2 ^ 12)]
    [InlineData(2 ^ 16)]
    [InlineData(2 ^ 17)]
    [InlineData(2 ^ 25)]
    public void ShouldReturnUtf8ByteArrayOfTrimmedString(int stringLength)
    {
        var randomString = RandomString(stringLength);
        var bytecode = _cut.ToByteCode(new BString($"'{randomString}'"));
        // assert
        bytecode
            .Should()
            .BeEquivalentTo(Encoding.UTF8.GetBytes(randomString));
        _byteCompiler.RecordedData.Should().BeSameAs(bytecode);
    }

    [Fact]
    public void EmptyStringIsInvalid() =>
        Assert.Throws<ParsingException>(() => _cut.ToByteCode("''"));

    private string RandomString(int stringLength)
    {
        var sb = new StringBuilder(stringLength);
        while (sb.Length < stringLength)
            sb.Append(_fixture.Create<string>());
        return sb.ToString().Substring(0, stringLength);
    }
}