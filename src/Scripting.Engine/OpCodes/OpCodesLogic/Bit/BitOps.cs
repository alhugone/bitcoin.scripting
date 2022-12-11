using System;
using System.Collections.Generic;
using Scripting.Engine.BStacks;
using Scripting.Engine.Number;
using Scripting.Engine.OpCodesLogic.Stack;

namespace Scripting.Engine.OpCodesLogic.Bit;

public static class BitOps
{
    public static void DoBinary(IBStack stack, Func<long, long, long> binaryOp)
    {
        var b = stack.PopInt32();
        var a = stack.PopInt32();
        stack.Push(new BInt64(binaryOp(a, b)));
    }

    public static void DoBinary(IBStack stack, Func<long, long, bool> binaryOp)
    {
        var a = stack.PopInt32();
        var b = stack.PopInt32();
        stack.Push(new BInt64(binaryOp(a, b) ? 1 : 0));
    }

    public static void DoBinaryWithVerify(IBStack stack, Action<IBStack> binaryOp)
    {
        binaryOp(stack);
        if (stack.PopInt32().ToInt() == 0)
            throw new Exception("");
    }

    public static void DoUnary(IBStack stack, Func<long, long> unaryOp)
    {
        var a = stack.PopInt32();
        stack.Push(new BInt64(unaryOp(a)));
    }

    public static void DoUnary(IBStack stack, Func<long, bool> unaryOp)
    {
        var a = stack.PopInt32();
        stack.Push(new BInt64(unaryOp(a) ? 1 : 0));
    }

    public static void Do3ArgsOperator(IBStack stack, Func<long, long, long, bool> op)
    {
        var c = stack.PopInt32();
        var b = stack.PopInt32();
        var a = stack.PopInt32();
        stack.Push(new BInt64(op(a, b, c) ? 1 : 0));
    }

    public static void OP_EqualVerify(IBStack stack)
    {
        OP_Equal(stack);
        var value = stack.Pop();
        if (value.AsBool())
            return;
        stack.Push(value);
        throw new Exception("veify");
    }

    public static void OP_Equal(IBStack stack)
    {
        var x1 = stack.Pop();
        var x2 = stack.Pop();
        var result = OP_Equal(x1, x2) ? 1 : 0;
        stack.Push(new BInt32(result));
    }

    public static bool OP_Equal(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
    {
        if (a.Count == b.Count)
        {
            for (var i = 0; i < a.Count; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }
        return false;
    }

    public static long Subtract(long a, long b) => a - b;
    public static long Increment(long a) => ++a;
    public static long Decrement(long a) => --a;
    public static long Negate(long a) => -a;
    public static long Abs(long a) => Math.Abs(a);
    public static bool IsZero(long a) => a == 0L;
    public static bool IsNotZero(long a) => a != 0L;
    public static bool OP_BOOLAND(long a, long b) => a != 0L && b != 0L;
    public static bool OP_BOOLOR(long a, long b) => a != 0L || b != 0L;
    public static bool OP_NUMEQUAL(long a, long b) => a == b;
    public static bool OP_NUMNOTEQUAL(long a, long b) => a != b;
    public static bool OP_LESSTHAN(long a, long b) => a < b;
    public static bool OP_GREATERTHAN(long a, long b) => a > b;
    public static bool OP_LESSTHANOREQUAL(long a, long b) => a <= b;
    public static bool OP_GREATERTHANOREQUAL(long a, long b) => a >= b;
    public static long OP_MIN(long a, long b) => Math.Min(a, b);
    public static long OP_MAX(long a, long b) => Math.Max(a, b);
    public static bool OP_WITHIN(long x, long min, long max) => min <= x && x <= max;
}
// OpSub = 0x94,
// OpMul = 0x95,
// OpDiv = 0x96,
// OpMod = 0x97,
// OpLshift = 0x98,
// OpRshift = 0x99,