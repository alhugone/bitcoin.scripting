using System.Text;
using AutoFixture;
using BtcScript.Compiler.Compiling.Parsing.Tokens;
using BtcScript.Compiler.Compiling.TokenCompilers;
using FluentAssertions;
using Xunit;

namespace BtcScript.Compiler.Tests;

public class BStringCompilerTests
{
    private readonly StringCompiler _cut = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldReturnUtf8ByteArray()
    {
        var randomString = _fixture.Create<string>();
        _cut.ToByteCode(new BString(randomString))
            .Should()
            .BeEquivalentTo(Encoding.UTF8.GetBytes(randomString));
    }
}
