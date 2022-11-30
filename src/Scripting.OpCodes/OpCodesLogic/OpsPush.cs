using System;
using Scripting.OpCodes.Number;

namespace Scripting.OpCodes.OpCodesLogic
{
    public static class OpsPush
    {
        public static void OP_0(IBStack stack)
        {
            stack.Push(Array.Empty<byte>());
        }
        public static void OP_1(IBStack stack)
        {
            OP_N(stack,1);
        }
        public static void OP_1NEGATE(IBStack stack)
        {
            OP_N(stack,-1);
        }

        public static void OP_N(IBStack stack, int n)
        {
            stack.Push(new BInt32(n));
        }public static void OP_PUSHDATA1(IBStack stack, Func<ulong,byte[]> readNextBytes)
        {
            OP_PUSHDATAN(stack,1,readNextBytes);
        }public static void OP_PUSHDATA2(IBStack stack, Func<ulong,byte[]> readNextBytes)
        {
            OP_PUSHDATAN(stack,1,readNextBytes);
        }public static void OP_PUSHDATA4(IBStack stack, Func<ulong,byte[]> readNextBytes)
        {
            OP_PUSHDATAN(stack,1,readNextBytes);
        }
        public static void OP_PUSHDATAN(IBStack stack,ulong n, Func<ulong,byte[]> readNextBytes)
        {
            ulong nextBytes = BitConverter.ToUInt64(readNextBytes(n));
            stack.Push(readNextBytes(nextBytes));
        }


        public static void OP_PushByteLen(IBStack stack, ulong n, Func<ulong,byte[]> readNextBytes)
        {
            stack.Push(readNextBytes(n));
        }
    }
}
