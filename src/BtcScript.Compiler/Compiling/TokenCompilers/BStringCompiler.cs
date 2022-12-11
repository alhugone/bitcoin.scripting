using System.Text;
using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class BStringCompiler : ITokenCompiler<BString>, ITokenCompiler<string>
{
    private readonly ITokenCompiler<byte[]> _bytesCompiler;
    public BStringCompiler(ITokenCompiler<byte[]> bytesCompiler) => _bytesCompiler = bytesCompiler;
    public BStringCompiler() => _bytesCompiler = new BytesCompiler();
    public byte[] ToByteCode(BString value) => ToByteCodeUnsafe(value.Value);
    public byte[] ToByteCode(string value) => ToByteCode(new BString(value));

    private byte[] ToByteCodeUnsafe(string bytes) =>
        _bytesCompiler.ToByteCode(Encoding.UTF8.GetBytes(bytes));
}