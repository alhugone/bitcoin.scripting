using System.Diagnostics.CodeAnalysis;

namespace BtcScript.Compiler.Compiling.Parsing.Tokens;

public class BMnemonic : BToken
{
    public BMnemonic(string mnemonic) => Value = BitcoinScriptMnemonics.GetOpCodeFromMnemonic(mnemonic);
    public OpCodeType Value { get; }

    public static bool TryParse(string token, [NotNullWhen(true)] out BMnemonic? mnemonic)
    {
        mnemonic = null;
        if (!BitcoinScriptMnemonics.GetMnemonics.Contains(token, StringComparer.OrdinalIgnoreCase))
            return false;
        mnemonic = new BMnemonic(token);
        return true;
    }
}
