using System;
using System.Collections.Generic;
using System.Linq;
using BtcScript.Compiler.Compiling.Parsing.Tokens;
using BtcScript.Compiler.Compiling.TokenCompilers;
using FluentAssertions;
using Xunit;

namespace BtcScript.Compiler.Tests.TokenCompilers;

public class BMnemonicCompilerTests
{
    private readonly BMnemonicCompiler _cut = new();

    public static IEnumerable<object[]> SetOfAllValidMnemonics =>
        BMnemonic
            .EnumerateAll()
            .Select(mnemonic => new[] { mnemonic });

    [Theory]
    [MemberData(nameof(SetOfAllValidMnemonics))]
    public void AllValidMnemonicShouldBeCompiledToOpCodeByName(string mnemonic)
    {
        var bMnemonic = new BMnemonic(mnemonic);
        var bytecode = _cut.ToByteCode(new BMnemonic(mnemonic));
        // assert
        bytecode
            .Should()
            .BeEquivalentTo(new[] { (byte)Enum.Parse<OpCodeType>(bMnemonic.Value) });
    }
}