using Scripting.OpCodes.Number;

namespace Scripting.OpCodes.OpCodesLogic.Splice
{
    public static class SpliceOps
    {
        public static void OP_SIZE(IBStack stack) => stack.Push(new BInt32(stack.Peek().Count));
    }
}