using System.Globalization;
using BtcScript.Compiler.Compiling.Parsing.Tokens;

namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class BHexCompiler : ITokenCompiler<BHex>
{
    internal const string HexPrefix = "0x";
    public byte[] ToByteCode(BHex value) => Serialize(value.Value);

    internal static byte[] Serialize(string value)
    {
        const int hexDigitsInByte = 2;
        var bytes = new List<byte>();
        if (value.StartsWith(HexPrefix, StringComparison.OrdinalIgnoreCase))
            value = value[2..];
        for (var i = 0; i + hexDigitsInByte <= value.Length; i += hexDigitsInByte)
            bytes.Add(
                byte.Parse(
                    value.Substring(i, hexDigitsInByte),
                    NumberStyles.HexNumber
                )
            );
        return bytes.ToArray();
    }
}