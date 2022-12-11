using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace BtcScript.Compiler.Compiling.Parsing.Tokens;

public class BHex : BToken
{
    public BHex(string hexString)
    {
        if (!IsHexString(hexString))
            throw ParsingException.NotHexString(hexString);
        Value = hexString;
    }

    public string Value { get; }

    public static bool IsHexString(string hexString) =>
        hexString.Length % 2 == 0 && Regex.IsMatch(
            hexString,
            "^(0x){1}([a-f0-9]{2})+$",
            RegexOptions.IgnoreCase
        );

    public static bool TryParse(string @string, [NotNullWhen(true)] out BHex? hex)
    {
        hex = null;
        if (!IsHexString(@string))
            return false;
        hex = new BHex(@string);
        return true;
    }
}