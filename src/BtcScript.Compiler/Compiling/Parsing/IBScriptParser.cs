using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.Parsing;

public interface IBScriptParser
{
    public IEnumerable<BToken> Parse(string script);
}