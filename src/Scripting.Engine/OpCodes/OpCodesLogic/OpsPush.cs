using System;
using Scripting.Engine.Number;

namespace Scripting.Engine.OpCodesLogic;

public static class OpsPush
{
    public static void OP_0(IBStack stack)
    {
        stack.Push(Array.Empty<byte>());
    }

    public static void OP_1(IBStack stack)
    {
        OP_N(stack, 1);
    }

    public static void OP_1NEGATE(IBStack stack)
    {
        OP_N(stack, -1);
    }

    public static void OP_N(IBStack stack, int n)
    {
        stack.Push(new BInt32(n));
    }

    public static void OP_PUSHDATA1(IBStack stack, Func<ulong, byte[]> readNextBytes)
    {
        OP_PUSHDATAN(stack, 1, readNextBytes);
    }

    public static void OP_PUSHDATA2(IBStack stack, Func<ulong, byte[]> readNextBytes)
    {
        OP_PUSHDATAN(stack, 2, readNextBytes);
    }

    public static void OP_PUSHDATA4(IBStack stack, Func<ulong, byte[]> readNextBytes)
    {
        OP_PUSHDATAN(stack, 4, readNextBytes);
    }

    public static void OP_PUSHDATAN(IBStack stack, ulong n, Func<ulong, byte[]> readNextBytes)
    {
        var bytes = readNextBytes(n);
        uint nextBytes = 0;
        if (bytes.Length == 1)
            nextBytes = bytes[0];
        else if (bytes.Length == 2)
            nextBytes = BitConverter.ToUInt16(bytes);
        else if (bytes.Length == 4)
            nextBytes = BitConverter.ToUInt32(bytes);
        stack.Push(readNextBytes(nextBytes));
    }

    public static void OP_PushByteLen(IBStack stack, ulong n, Func<ulong, byte[]> readNextBytes)
    {
        stack.Push(readNextBytes(n));
    }
}