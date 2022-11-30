using System.Diagnostics.CodeAnalysis;

namespace BtcScript.Compiler.Compiling.Parsing.Tokens;

public class BString : BToken
{
    public BString(string @string) => Value = @string;
    public string Value { get; }

    public static bool TryParse(string token, [NotNullWhen(true)] out BString? bstring)
    {
        bstring = null;
        if (!IsString(token))
            return false;
        bstring = new BString(token);
        return true;
    }

    internal static bool IsString(string token) => (token.StartsWith("'") && token.EndsWith("'"));
}
