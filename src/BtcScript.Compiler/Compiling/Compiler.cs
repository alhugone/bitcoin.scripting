using BtcScript.Compiler.Compiling.Parsing;
using BtcScript.Compiler.Compiling.TokenCompilers;

namespace BtcScript.Compiler.Compiling;

public class Compiler
{
    private readonly IBScriptParser _bScriptIbScriptParser;
    private readonly BTokensCompiler _tokensCompiler;

    public Compiler(BTokensCompiler tokensCompiler, IBScriptParser bScriptIbScriptParser)
    {
        _tokensCompiler = tokensCompiler;
        _bScriptIbScriptParser = bScriptIbScriptParser;
    }

    public byte[] Compile(string script)
    {
        using var stream = new MemoryStream();
        foreach (var token in _bScriptIbScriptParser.Parse(script))
            stream.Write(_tokensCompiler.ToByteCode(token));
        return stream.ToArray();
    }
}
