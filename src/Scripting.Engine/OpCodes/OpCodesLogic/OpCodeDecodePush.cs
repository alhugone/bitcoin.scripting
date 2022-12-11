using System;
using System.Collections.Generic;

namespace Scripting.Engine.OpCodesLogic;

public class OpCodeDecodePush
{
    public static IReadOnlyList<byte[]> OP_PUSHDATA1(Func<int, byte[]> readNextBytes) => OP_PUSHDATAN(1, readNextBytes);
    public static IReadOnlyList<byte[]> OP_PUSHDATA2(Func<int, byte[]> readNextBytes) => OP_PUSHDATAN(2, readNextBytes);
    public static IReadOnlyList<byte[]> OP_PUSHDATA4(Func<int, byte[]> readNextBytes) => OP_PUSHDATAN(4, readNextBytes);

    public static IReadOnlyList<byte[]> OP_PUSHDATAN(int n, Func<int, byte[]> readNextBytes)
    {
        var bytes = readNextBytes(n);
        var nextBytes = 0;
        if (bytes.Length == 1)
            nextBytes = bytes[0];
        else if (bytes.Length == 2)
            nextBytes = BitConverter.ToInt16(bytes);
        else
            throw new NotSupportedException();
        return new List<byte[]> { bytes, readNextBytes(nextBytes) };
    }

    public static IEnumerable<byte[]> OP_ImmediatelyN(int bytes, Func<int, byte[]> readNextBytes) =>
        new List<byte[]> { readNextBytes(bytes) };
}