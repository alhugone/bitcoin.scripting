using System;

namespace Scripting.Engine.OpCodesLogic.Arithmetic;

public class ArithmeticOpsCompose
{
    public static Action<IBStack> Add = stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.Add);
    public static Action<IBStack> Subtract = stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.Subtract);
    public static Action<IBStack> Increment = stack => ArithmeticOps.DoUnary(stack, ArithmeticOps.Increment);
    public static Action<IBStack> Decrement = stack => ArithmeticOps.DoUnary(stack, ArithmeticOps.Decrement);
    public static Action<IBStack> Negate = stack => ArithmeticOps.DoUnary(stack, ArithmeticOps.Negate);
    public static Action<IBStack> Abs = stack => ArithmeticOps.DoUnary(stack, ArithmeticOps.Abs);
    public static Action<IBStack> OP_NOT = stack => ArithmeticOps.DoUnary(stack, ArithmeticOps.IsZero);
    public static Action<IBStack> OP_0NOTEQUAL = stack => ArithmeticOps.DoUnary(stack, ArithmeticOps.IsNotZero);
    public static Action<IBStack> OP_BOOLAND = stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_BOOLAND);
    public static Action<IBStack> OP_BOOLOR = stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_BOOLOR);
    public static Action<IBStack> OP_NUMEQUAL = stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_NUMEQUAL);

    public static Action<IBStack> OP_NUMNOTEQUAL =
        stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_NUMNOTEQUAL);

    public static Action<IBStack> OP_LESSTHAN = stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_LESSTHAN);

    public static Action<IBStack> OP_GREATERTHAN =
        stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_GREATERTHAN);

    public static Action<IBStack> OP_LESSTHANOREQUAL =
        stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_LESSTHANOREQUAL);

    public static Action<IBStack> OP_GREATERTHANOREQUAL =
        stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_GREATERTHANOREQUAL);

    public static Action<IBStack> OP_MIN = stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_MIN);
    public static Action<IBStack> OP_MAX = stack => ArithmeticOps.DoBinary(stack, ArithmeticOps.OP_MAX);

    public static Action<IBStack>
        OP_WITHIN = stack => ArithmeticOps.Do3ArgsOperator(stack, ArithmeticOps.OP_WITHIN);

    public static Action<IBStack> OP_NUMEQUALVERIFY = stack =>
        ArithmeticOps.DoBinaryWithVerify(stack, s => ArithmeticOps.DoBinary(s, ArithmeticOps.OP_NUMEQUAL));
}