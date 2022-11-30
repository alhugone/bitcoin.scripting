using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.Parsing;

public class BScriptParser : IBScriptParser
{
    private static readonly string[] TokensSeparators = { " ", "\r\n", "\n", "\t" };

    public IEnumerable<BToken> Parse(string script) =>
        script.Split(
                TokensSeparators,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            )
            .Select(GetTokenType);

    internal static BToken GetTokenType(string token)
    {
        if (BNumber.TryParse(token, out var bNumber))
            return bNumber;
        if (BHex.TryParse(token, out var bHex))
            return bHex;
        if (BString.TryParse(token, out var bString))
            return bString;
        if (BMnemonic.TryParse(token, out var bOpCode))
            return bOpCode;
        throw ParsingException.UnRecognizedToken(token);
    }
}
