using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class BMnemonicCompiler : ITokenCompiler<BMnemonic>
{
    public byte[] ToByteCode(BMnemonic bToken) => ToByteCode(bToken.Value);
    private byte[] ToByteCode(string bMnemonic) => new[] { (byte)Enum.Parse<OpCodeType>(bMnemonic, true) };
}