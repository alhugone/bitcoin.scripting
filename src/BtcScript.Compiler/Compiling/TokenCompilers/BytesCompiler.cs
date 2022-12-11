namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class BytesCompiler : ITokenCompiler<byte[]>
{
    public byte[] ToByteCode(byte[] value)
    {
        const int maxPrefixBytes = 5;
        if (BitConverter.IsLittleEndian == false)
            throw new PlatformNotSupportedException("Current CPU Architecture is not Not Little endian based.");
        var list = new List<byte>(value.Length + maxPrefixBytes);
        switch (value.Length)
        {
            case < (int)OpCodeType.OP_PUSHDATA1:
                list.Add((byte)value.Length);
                break;
            case <= 0xFF:
                list.Add((byte)OpCodeType.OP_PUSHDATA1);
                list.Add((byte)value.Length);
                break;
            case <= 0xFFFF:
            {
                list.Add((byte)OpCodeType.OP_PUSHDATA2);
                list.AddRange(BitConverter.GetBytes((short)value.Length));
                break;
            }
            default:
            {
                list.Add((byte)OpCodeType.OP_PUSHDATA4);
                list.AddRange(BitConverter.GetBytes(value.Length));
                break;
            }
        }
        list.AddRange(value);
        return list.ToArray();
    }
}