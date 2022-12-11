using System;

namespace BtcScript.Compiler.Compiling.Parsing;

public class ParsingException : Exception
{
    private ParsingException(string message) : base("script parse error: " + message)
    {
    }

    public static ParsingException NumberOutOfRange(long number) =>
        new(
            $"'{number}' is too big. decimal numeric values are only allowed in the range {-0xFFFFFFFF}...{0xFFFFFFFF}"
        );

    public static ParsingException NotHexString(string token) =>
        new($"'{token}' is not valid HEX. It should be nonempty string prefixed with 0x [A..F][0..9]");

    public static ParsingException UnRecognizedToken(string token) =>
        new($"Token '{token}' could not be parsed as any Bitcoin Script valid token");

    public static ParsingException NotString(string token)
        => new($"'{token}' is not valid Bitcoin Script String. It should be nonempty string enclosed in ''");
}