using BtcScript.Compiler.Compiling.Parsing;
using BtcScript.Compiler.Compiling.Parsing.Tokens;
using BtcScript.Compiler.Compiling.TokenCompilers;

namespace BtcScript.Compiler.Compiling;

public static class BScriptCompilerFactory
{
    public static BScriptCompiler GetDefault() => new(
        new AnyBTokenCompiler(
            new BStringCompiler(),
            new BHexCompiler(),
            new BNumberCompiler(new BytesCompiler()),
            new BMnemonicCompiler()
        ),
        new BScriptParser()
    );
}

public class BScriptCompiler
{
    private readonly IBScriptParser _bScriptIbScriptParser;
    private readonly ITokenCompiler<BToken> _tokensCompiler;

    public BScriptCompiler(
        ITokenCompiler<BToken> tokensCompiler,
        IBScriptParser bScriptIbScriptParser
    )
    {
        _tokensCompiler = tokensCompiler;
        _bScriptIbScriptParser = bScriptIbScriptParser;
    }

    public IEnumerable<byte> Compile(string script) =>
        _bScriptIbScriptParser
            .Parse(script)
            .SelectMany(_tokensCompiler.ToByteCode);
}