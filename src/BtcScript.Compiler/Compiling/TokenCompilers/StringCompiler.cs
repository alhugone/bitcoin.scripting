using System.Text;
using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class StringCompiler : ITokenCompiler<BString>, ITokenCompiler<string>
{
    private readonly ITokenCompiler<byte[]> _bytesCompiler;
    public StringCompiler(ITokenCompiler<byte[]> bytesCompiler) => _bytesCompiler = bytesCompiler;
    public StringCompiler() => _bytesCompiler = new BytesCompiler();
    public byte[] ToByteCode(BString number) => ToByteCode(number.Value);

    public byte[] ToByteCode(string number) =>
        _bytesCompiler.ToByteCode(Encoding.UTF8.GetBytes(number));
}
