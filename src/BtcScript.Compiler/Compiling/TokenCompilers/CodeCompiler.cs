using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class CodeCompiler : ITokenCompiler<BMnemonic>
{
    public byte[] ToByteCode(BMnemonic number) => Serialize(number.Value);
    private byte[] Serialize(OpCodeType opcodeValue) => new[] { (byte)opcodeValue };
}