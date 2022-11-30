using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class BTokensCompiler
{
    private readonly ITokenCompiler<BString> _bStringCompiler;
    private readonly ITokenCompiler<BHex> _hexCompiler;
    private readonly ITokenCompiler<BNumber> _numberCompiler;
    private readonly ITokenCompiler<BMnemonic> _opCodeCompiler;

    public BTokensCompiler(
        ITokenCompiler<BString> bStringCompiler,
        ITokenCompiler<BHex> hexCompiler,
        ITokenCompiler<BNumber> numberCompiler,
        ITokenCompiler<BMnemonic> opCodeCompiler
    )
    {
        _bStringCompiler = bStringCompiler;
        _hexCompiler = hexCompiler;
        _numberCompiler = numberCompiler;
        _opCodeCompiler = opCodeCompiler;
    }

    public byte[] ToByteCode(BToken token)
    {
        switch (token)
        {
            case BNumber number:
                return _numberCompiler.ToByteCode(number);
            case BHex hex:
                return _hexCompiler.ToByteCode(hex);
            case BString str:
                return _bStringCompiler.ToByteCode(str);
            case BMnemonic opcode:
                return _opCodeCompiler.ToByteCode(opcode);
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(token),
                    token.GetType().Name,
                    "Token type is not known to Serializer"
                );
        }
    }
}
