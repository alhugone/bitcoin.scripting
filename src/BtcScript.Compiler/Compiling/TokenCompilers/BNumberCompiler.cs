using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class BNumberCompiler : ITokenCompiler<BNumber>, ITokenCompiler<long>
{
    private readonly ITokenCompiler<byte[]> _bytesCompiler;
    public BNumberCompiler(ITokenCompiler<byte[]> bytesCompiler) => _bytesCompiler = bytesCompiler;
    public BNumberCompiler() => _bytesCompiler = new BytesCompiler();
    public byte[] ToByteCode(BNumber value) => ToByteCode(value.Value);

    public byte[] ToByteCode(long value)
    {
        return value switch
        {
            -1 or >= 1 and <= 16 => new[]
            {
                (byte)((byte)value + (OpCodeType.OP_1 - 1))
            },
            0 => new[]
            {
                (byte)OpCodeType.OP_0
            },
            _ => _bytesCompiler.ToByteCode(IntegerSerializer.Serialize(value))
        };
    }
}