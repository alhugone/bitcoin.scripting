using Scripting.Engine.Number;

namespace Scripting.Engine.OpCodesLogic.Splice;

public static class SpliceOps
{
    public static void OP_SIZE(IBStack stack) => stack.Push(new BInt32(stack.Count));
}