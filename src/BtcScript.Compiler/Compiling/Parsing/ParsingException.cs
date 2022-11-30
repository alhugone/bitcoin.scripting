namespace BtcScript.Compiler.Compiling.Parsing;

public class ParsingException : Exception
{
    private ParsingException(string message) : base(message)
    {
    }

    public static ParsingException NumberOutOfRange(long number) =>
        new(
            $"script parse error: '{number}' decimal numeric value only allowed in the range {-0xFFFFFFFF}...{0xFFFFFFFF}"
        );

    public static ParsingException NotHexString(string token) =>
        new($"script parse error: '{token}' is not valid HEX");

    public static ParsingException UnRecognizedToken(string token) =>
        new($"Token <{token}> could not be parsed as Bitcoin Script valid token");
}
