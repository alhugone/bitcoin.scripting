using BtcScript.Compiler.Compiling.TokenCompilers;

namespace BtcScript.Compiler.Tests.TokenCompilers;

public class DummyByteCompiler : ITokenCompiler<byte[]>
{
    public byte[]? RecordedData { get; private set; }

    public byte[] ToByteCode(byte[] value)
    {
        RecordedData = value;
        return value;
    }
}