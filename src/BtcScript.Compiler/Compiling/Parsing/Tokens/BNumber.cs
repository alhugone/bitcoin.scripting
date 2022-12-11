using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace BtcScript.Compiler.Compiling.Parsing.Tokens;

public class BNumber : BToken
{
    public BNumber(string @string)
    {
        var integer = long.Parse(@string);
        if (integer is < -1 * 0xFFFFFFFF or > 0xFFFFFFFF)
            throw ParsingException.NumberOutOfRange(integer);
        Value = integer;
    }

    public long Value { get; }

    public static bool TryParse(string @string, [NotNullWhen(true)] out BNumber? number)
    {
        if (!IsNumber(@string))
        {
            number = null;
            return false;
        }
        number = new BNumber(@string);
        return true;
    }

    private static bool IsNumber(string token) => Regex.IsMatch(token, "^-?[0-9]+$");
}