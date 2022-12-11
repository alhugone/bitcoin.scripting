namespace BtcScript.Compiler.Compiling.TokenCompilers;

public interface ITokenCompiler<in T>
{
    byte[] ToByteCode(T value);
}