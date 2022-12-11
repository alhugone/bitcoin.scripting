using System.Diagnostics.CodeAnalysis;

namespace BtcScript.Compiler.Compiling.Parsing.Tokens;

public class BString : BToken
{
    public BString(string @string) =>
        Value = IsString(@string) ? @string.Trim('\'') : throw ParsingException.NotString(@string);

    public string Value { get; }

    public static bool TryParse(string token, [NotNullWhen(true)] out BString? bString)
    {
        bString = null;
        if (!IsString(token))
            return false;
        bString = new BString(token);
        return true;
    }

    internal static bool IsString(string token) => token.Length > 2 && token.StartsWith("'") && token.EndsWith("'");
}