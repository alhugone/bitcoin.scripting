namespace BtcScript.Compiler.Compiling.TokenCompilers;

public static class IntegerSerializer
{
    public static byte[] Serialize(long value)
    {
        if (value == long.MinValue)
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                $"Numbers can be only in range: <{long.MinValue + 1};{long.MaxValue}>"
            );
        if (value == 0)
            return Array.Empty<byte>();
        var result = new List<byte>();
        var isNegative = value < 0;
        var absValue = isNegative ? ~(ulong)value + 1 : (ulong)value;
        while (absValue != 0)
        {
            result.Add((byte)(absValue & 0xff));
            absValue >>= 8;
        }
        if ((result.Last() & 0x80) != 0)
            result.Add(isNegative ? (byte)0x80 : (byte)0);
        else if (isNegative)
            result[^1] |= 0x80;
        return result.ToArray();
    }

    public static long Deserialize(IReadOnlyList<byte> vch)
    {
        if (!vch.Any())
            return 0;
        long result = 0;
        for (var i = 0; i != vch.Count; ++i)
            result |= (long)vch[i] << (8 * i);

        // If the input vector's most significant byte is 0x80, remove it from
        // the result's msb and return a negative.
        if ((vch.Last() & 0x80) != 0)
            return -(long)((ulong)result & ~(0x80UL << (8 * (vch.Count - 1))));
        return result;
    }
}