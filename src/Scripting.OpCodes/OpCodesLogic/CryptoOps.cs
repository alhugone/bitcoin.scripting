using System;
using System.Linq;
using System.Security.Cryptography;
using Scripting.OpCodes.Number;

namespace Scripting.OpCodes.OpCodesLogic
{
    public static class CryptoOps
    {
        public static void Sha1(IBStack stack)
        {
            stack.Push(SHA1.HashData(stack.Pop().ToArray()));
        }

        public static void Sha256(IBStack stack)
        {
            stack.Push(SHA256.HashData(stack.Pop().ToArray()));
        } public static void OP_HASH160(IBStack stack)
        {
        }public static void OP_HASH256(IBStack stack)
        {
            stack.Push(SHA256.HashData(SHA256.HashData(stack.Pop().ToArray())));
        }public static void OP_CODESEPARATOR(IBStack stack)
        {

        }

        public static void RIPEMD160(IBStack stack)
        {
            //stack.Push(System.Security.Cryptography.rip.HashData(stack.Pop().ToArray()));
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
            OP_PUSHDATAN(stack, 1, readNextBytes);
        }

        public static void OP_PUSHDATA4(IBStack stack, Func<ulong, byte[]> readNextBytes)
        {
            OP_PUSHDATAN(stack, 1, readNextBytes);
        }

        public static void OP_PUSHDATAN(IBStack stack, ulong n, Func<ulong, byte[]> readNextBytes)
        {
            var nextBytes = BitConverter.ToUInt64(readNextBytes(n));
            stack.Push(readNextBytes(nextBytes));
        }

        public static void OP_PushByteLen(IBStack stack, ulong n, Func<ulong, byte[]> readNextBytes)
        {
            stack.Push(readNextBytes(n));
        }
    }
}
