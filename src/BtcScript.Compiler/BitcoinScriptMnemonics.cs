namespace BtcScript.Compiler;

public static class BitcoinScriptMnemonics
{
    public static IReadOnlySet<string> GetMnemonics { get; } = Enum.GetNames<OpCodeType>().ToHashSet();
    public static OpCodeType GetOpCodeFromMnemonic(string mnemonic) => Enum.Parse<OpCodeType>(mnemonic);
}