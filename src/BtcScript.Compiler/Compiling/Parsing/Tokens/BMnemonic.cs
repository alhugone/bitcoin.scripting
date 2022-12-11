using System.Diagnostics.CodeAnalysis;

namespace BtcScript.Compiler.Compiling.Parsing.Tokens;

public class BMnemonic : BToken
{
    public BMnemonic(string mnemonic) => Value = mnemonic;
    public string Value { get; }

    public static bool TryParse(string token, [NotNullWhen(true)] out BMnemonic? mnemonic)
    {
        mnemonic = null;
        var normalizedToken = Normalize(token);
        if (!BitcoinScriptMnemonics.GetMnemonics.Contains(normalizedToken, StringComparer.OrdinalIgnoreCase))
            return false;
        mnemonic = new BMnemonic(normalizedToken);
        return true;
    }

    private static string Normalize(string token)
    {
        if (token.StartsWith("OP", StringComparison.OrdinalIgnoreCase))
            token = token.Substring(2);
        token = token.Replace("_", "", StringComparison.OrdinalIgnoreCase);
        token = "OP_" + token;
        return token;
    }

    public static IEnumerable<string> EnumerateAll() =>
        BitcoinScriptMnemonics.GetMnemonics;

    private static class BitcoinScriptMnemonics
    {
        public static IReadOnlySet<string> GetMnemonics { get; } = Enum.GetNames<OpCodeType>().ToHashSet();
    }
}