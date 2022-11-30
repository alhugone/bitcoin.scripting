namespace BtcScript.Compiler.Compiling.TokenCompilers;

public class IntegerSerializer
{
    public static byte[] serialize(long value)
    {
        if (value == 0)
            return Array.Empty<byte>();
        var result = new List<byte>();
        var neg = value < 0;
        var absvalue = neg ? ~(ulong)value + 1 : (ulong)value;
        while (absvalue != 0)
        {
            result.Add((byte)(absvalue & 0xff));
            absvalue >>= 8;
        }

//    - If the most significant byte is >= 0x80 and the value is positive, push a
//    new zero-byte to make the significant byte < 0x80 again.

//    - If the most significant byte is >= 0x80 and the value is negative, push a
//    new 0x80 byte that will be popped off when converting to an integral.

//    - If the most significant byte is < 0x80 and the value is negative, add
//    0x80 to it, since it will be subtracted and interpreted as a negative when
//    converting to an integral.
        if ((result.Last() & 0x80) != 0)
            result.Add(neg ? (byte)0x80 : (byte)0);
        else if (neg)
            result[^1] |= 0x80;
        return result.ToArray();
    }
}