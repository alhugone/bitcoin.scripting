using System.Diagnostics.CodeAnalysis;
using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.TokenCompilers;

public static class AnyBTokenCompilerFactory
{
    public static AnyBTokenCompiler GetDefault() => new(
        new BStringCompiler(new BytesCompiler()),
        new BHexCompiler(),
        new BNumberCompiler(new BytesCompiler()),
        new BMnemonicCompiler()
    );
}

public class AnyBTokenCompiler : ITokenCompiler<BToken>
{
    private readonly ITokenCompiler<BString> _bStringCompiler;
    private readonly ITokenCompiler<BHex> _hexCompiler;
    private readonly ITokenCompiler<BNumber> _numberCompiler;
    private readonly ITokenCompiler<BMnemonic> _opCodeCompiler;

    public AnyBTokenCompiler(
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

    public byte[] ToByteCode(BToken bToken)
    {
        if (!TryToByteCode(bToken, out var bytes))
            throw new ArgumentOutOfRangeException(
                nameof(bToken),
                bToken.GetType().Name,
                "Token type is not known to Compiler"
            );
        return bytes;
    }

    public virtual bool TryToByteCode(BToken bToken, [NotNullWhen(true)] out byte[]? bytes)
    {
        bytes = bToken switch
        {
            BNumber number => _numberCompiler.ToByteCode(number),
            BHex hex => _hexCompiler.ToByteCode(hex),
            BString str => _bStringCompiler.ToByteCode(str),
            BMnemonic opcode => _opCodeCompiler.ToByteCode(opcode),
            _ => null
        };
        return bytes is not null;
    }
}