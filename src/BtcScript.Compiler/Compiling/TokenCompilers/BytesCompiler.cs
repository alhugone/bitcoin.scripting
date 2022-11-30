namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class BytesCompiler : ITokenCompiler<byte[]>
{
    public byte[] ToByteCode(byte[] number)
    {
        if (BitConverter.IsLittleEndian == false)
            throw new Exception("Not Little endian");
        var list = new List<byte>(number.Length);
        switch (number.Length)
        {
            case < (int)OpCodeType.OP_PUSHDATA1:
                list.Add((byte)number.Length);
                break;
            case <= 0xff:
                list.Add((byte)OpCodeType.OP_PUSHDATA1);
                list.Add((byte)number.Length);
                break;
            case <= 0xffff:
            {
                list.Add((byte)OpCodeType.OP_PUSHDATA2);
                list.AddRange(BitConverter.GetBytes((short)number.Length));
                break;
            }
            default:
            {
                list.Add((byte)OpCodeType.OP_PUSHDATA4);
                list.AddRange(BitConverter.GetBytes(number.Length));
                break;
            }
        }
        list.AddRange(number);
        return list.ToArray();
    }
}
