using Scripting.OpCodes.Number;

namespace Scripting.OpCodes.BStacks
{
    public static class BStackExtensions
    {
        public static BInt32 PopInt32(this IBStack stack)
        {
            return new(stack.Pop());
        }

        public static BInt32 PopVerIfInt32(this IBStack stack)
        {
            return new(stack.Pop());
        }

        public static BInt32 PeekInt32(this IBStack stack)
        {
            return new(stack.Peek());
        }

        public static BInt64 PeekInt64(this IBStack stack)
        {
            return new(stack.Peek());
        }
    }
}